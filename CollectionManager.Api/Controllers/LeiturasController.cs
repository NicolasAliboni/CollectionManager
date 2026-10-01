using CollectionManager.Api.Data;
using CollectionManager.Api.DTOs;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LeiturasController : ControllerBase
{
    private readonly AppDbContext _context;

    public LeiturasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int colecaoLeituraId)
    {
        var colecaoExiste = await _context.ColecaoLeitura
            .AnyAsync(c => c.Id == colecaoLeituraId);
        if (!colecaoExiste)
        {
            return NotFound("Coleção não encontrada.");
        }

        var leituras = await _context.Leituras
            .Where(l => l.ColecaoLeituraId == colecaoLeituraId)
            .Include(l => l.Estado)
            .OrderBy(l => l.Volume)
            .ToListAsync();

        return Ok(leituras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leitura = await _context.Leituras
            .Include(l => l.Estado)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (leitura == null)
        {
            return NotFound("Leitura não encontrada");
        }

        return Ok(leitura);
    }

    [HttpPost]
    public async Task<IActionResult> Post(LeituraCreateDto dto)
    {
        var colecaoLeituraExistente = await _context.ColecaoLeitura
            .FindAsync(dto.ColecaoLeituraId);

        if (colecaoLeituraExistente == null)
        {
            return NotFound("Coleção não Encontrada");
        }

        if (!Enum.IsDefined(typeof(StatusLeitura), dto.Status))
        {
            return BadRequest("Status de leitura inválido.");
        }

        var estadoExiste = await _context.Estados
           .AnyAsync(e => e.Id == dto.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não Encontrado");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var leitura = new Leitura
            {
                ColecaoLeituraId = dto.ColecaoLeituraId,
                Titulo = dto.Titulo,
                DataLancamento = dto.DataLancamento,
                DataAquisicao = dto.DataAquisicao,
                Status = dto.Status,
                EstadoId = dto.EstadoId,
                CodigoEAN = dto.CodigoEAN,
                ISBN13 = dto.ISBN13,
                Volume = dto.Volume,
                ValorAquisicao = dto.ValorAquisicao,
                Observacoes = dto.Observacoes
            };

            _context.Leituras.Add(leitura);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, new
            {
                leitura.Id,
                leitura.Volume,
            }
            );
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LeituraCreateDto dto)
    {
        var leituraExistente = await _context.Leituras.FindAsync(id);

        if (leituraExistente == null)
        {
            return NotFound("Leitura não encontrada.");
        }

        if (dto.ColecaoLeituraId != leituraExistente.ColecaoLeituraId)
        {
            return BadRequest("Não é permitido alterar a coleção do volume.");
        }

        if (!Enum.IsDefined(typeof(StatusLeitura), dto.Status))
        {
            return BadRequest("Status de leitura inválido.");
        }

        var estadoExiste = await _context.Estados
            .AnyAsync(e => e.Id == dto.EstadoId);
        if (!estadoExiste)
        {
            return BadRequest("Estado não Encontrado");
        }

        leituraExistente.Titulo = dto.Titulo;
        leituraExistente.DataLancamento = dto.DataLancamento;
        leituraExistente.DataAquisicao = dto.DataAquisicao;
        leituraExistente.Status = dto.Status;
        leituraExistente.EstadoId = dto.EstadoId;
        leituraExistente.CodigoEAN = dto.CodigoEAN;
        leituraExistente.ISBN13 = dto.ISBN13;
        leituraExistente.Volume = dto.Volume;
        leituraExistente.ValorAquisicao = dto.ValorAquisicao;
        leituraExistente.Observacoes = dto.Observacoes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var leitura = await _context.Leituras.FindAsync(id);

        if (leitura == null)
        {
            return NotFound("Leitura não Encontrado");
        }

        _context.Leituras.Remove(leitura);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}