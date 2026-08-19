using CollectionManager.Api.Data;
using CollectionManager.Api.DTOs;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class JogosController : ControllerBase
{
    private readonly AppDbContext _context;

    public JogosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var jogos = await _context.Jogos
            .Include(j => j.Item)
                .ThenInclude(i => i.Estado)
            .Include(j => j.Item)
                .ThenInclude(i => i.Franquia)
            .Include(j => j.Marca)
            .Include(j => j.Plataforma)
            .Include(j => j.Status)
            .ToListAsync();

        return Ok(jogos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var jogo = await _context.Jogos
            .Include(c => c.Item)
                .ThenInclude(i => i.Estado)
            .Include(c => c.Item)
                .ThenInclude(i => i.Franquia)
            .Include(c => c.Marca)
            .Include(c => c.Plataforma)
            .Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.ItemId == id);

        if (jogo == null)
        {
            return NotFound("Jogo não Encontrado");
        }

        return Ok(jogo);
    }

    [HttpPost]
    public async Task<IActionResult> Post(JogoCreateDto dto)
    {
        var estadoExiste = await _context.Estados
            .AnyAsync(e => e.Id == dto.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não Encontrado");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias
                .AnyAsync(f => f.Id == dto.FranquiaId.Value);
            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var marcaExiste = await _context.Marcas
            .AnyAsync(m => m.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não Encontrada");
        }

        var plataformaExiste = await _context.Plataformas
            .AnyAsync(p => p.Id == dto.PlataformaId);

        if (!plataformaExiste)
        {
            return BadRequest("Plataforma não Encontrada");
        }

        var statusExiste = await _context.Status
            .AnyAsync(s => s.Id == dto.StatusId);

        if (!statusExiste)
        {
            return BadRequest("Status não Encontrada");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var item = new Item
            {
                Nome = dto.Nome,
                DataLancamento = dto.DataLancamento,
                EstadoId = dto.EstadoId,
                CodigoEAN = dto.CodigoEAN,
                DataAquisicao = dto.DataAquisicao,
                ValorAquisicao = dto.ValorAquisicao,
                FranquiaId = dto.FranquiaId,
                Observacoes = dto.Observacoes
            };

            _context.Itens.Add(item);

            await _context.SaveChangesAsync();

            var jogo = new Jogo
            {
                ItemId = item.Id,
                MarcaId = dto.MarcaId,
                PlataformaId = dto.PlataformaId,
                StatusId = dto.StatusId
            };

            _context.Jogos.Add(jogo);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, new
            {
                item.Id,
                item.Nome,
            }
            );
        }
        catch
        {   await transaction.RollbackAsync();
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(JogoCreateDto dto, int id)
    {
        var itemExistente = await _context.Itens.FindAsync(id);
        var jogoExistente = await _context.Jogos.FindAsync(id);

        if (itemExistente == null)
        {
            return NotFound("Item do Jogo não Encontrado");
        }

        if (jogoExistente == null)
        {
            return NotFound("Jogo não Encontrado");
        }

        var estadoExiste = await _context.Estados
            .AnyAsync(e => e.Id == dto.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não Encontrado");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias
                .AnyAsync(f => f.Id == dto.FranquiaId);
            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var marcaExiste = await _context.Marcas
            .AnyAsync(m => m.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não Encontrada");
        }

        var plataformaExiste = await _context.Plataformas
            .AnyAsync(p => p.Id == dto.PlataformaId);

        if (!plataformaExiste)
        {
            return BadRequest("Plataforma não Encontrada");
        }

        var statusExiste = await _context.Status
            .AnyAsync(s => s.Id == dto.StatusId);

        if (!statusExiste)
        {
            return BadRequest("Status não encontrado");
        }

        itemExistente.Nome = dto.Nome;
        itemExistente.DataLancamento = dto.DataLancamento;
        itemExistente.EstadoId = dto.EstadoId;
        itemExistente.CodigoEAN = dto.CodigoEAN;
        itemExistente.DataAquisicao = dto.DataAquisicao;
        itemExistente.ValorAquisicao = dto.ValorAquisicao;
        itemExistente.FranquiaId = dto.FranquiaId;
        itemExistente.Observacoes = dto.Observacoes;
        jogoExistente.MarcaId = dto.MarcaId;
        jogoExistente.PlataformaId = dto.PlataformaId;
        jogoExistente.StatusId = dto.StatusId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Itens.FindAsync(id);
        var jogo = await _context.Jogos.FindAsync(id);

        if (item == null)
        {
            return NotFound("Item do Jogo não encontrado");
        }

        if (jogo == null)
        {
            return NotFound("Jogo não encontrado");
        }

        _context.Jogos.Remove(jogo);
        _context.Itens.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
