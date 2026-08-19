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
    public async Task<IActionResult> Get()
    {
        var leituras = await _context.Leituras
            .Include(l => l.Item)
                .ThenInclude(i => i.Estado)
            .Include(l => l.Item)
                .ThenInclude(i => i.Franquia)
            .Include(l => l.EditoraExterior)
            .Include(l => l.EditoraBrasil)
            .Include(l => l.Status)
            .ToListAsync();

        return Ok(leituras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leitura = await _context.Leituras
            .Include(l => l.Item)
                .ThenInclude(i => i.Estado)
            .Include(l => l.Item)
                .ThenInclude(i => i.Franquia)
            .Include(l => l.EditoraExterior)
            .Include(l => l.EditoraBrasil)
            .Include(l => l.Status)
            .FirstOrDefaultAsync(l => l.ItemId == id);

        if (leitura == null)
        {
            return NotFound("Leitura não encontrada");
        }

        return Ok(leitura);
    }

    [HttpPost]
    public async Task<IActionResult> Post(LeituraCreateDto dto)
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
            .AnyAsync(e => e.Id == dto.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var editoraExteriorExiste = await _context.Editoras
            .AnyAsync(e =>e.Id == dto.EditoraExteriorId && e.Origem == OrigemEditora.Exterior);

        if (!editoraExteriorExiste)
        {
            return BadRequest("Editora Exterior não Encontrada");
        }

        var editoraBrasilExiste = await _context.Editoras
            .AnyAsync(e =>e.Id == dto.EditoraBrasilId && e.Origem == OrigemEditora.Brasil);

        if (!editoraBrasilExiste)
        {
            return BadRequest("Editora Brasil não Encontrada");
        }

        var statusExiste = await _context.Status
            .AnyAsync(e => e.Id == dto.StatusId);

        if (!statusExiste)
        {
            return BadRequest("Status não Encontrado");
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

            var leitura = new Leitura
            {
                ItemId = item.Id,
                Tipo = dto.Tipo,
                EditoraExteriorId = dto.EditoraExteriorId,
                EditoraBrasilId = dto.EditoraBrasilId,
                Autor = dto.Autor,
                StatusId = dto.StatusId,
                Lingua = dto.Lingua,
                ISBN13 = dto.ISBN13,
                Volume = dto.Volume,
                VolumeAte = dto.VolumeAte
            };

            _context.Leituras.Add(leitura);

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
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LeituraCreateDto dto)
    {
        var itemExistente = await _context.Itens.FindAsync(id);
        var leituraExistente = await _context.Leituras.FindAsync(id);

        if (itemExistente == null)
        {
            return NotFound("Item de Leitura não Encontrado");
        }

        if (leituraExistente == null)
        {
            return NotFound("Leitura não Encontrada");
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
            .AnyAsync(e => e.Id == dto.FranquiaId);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var editoraExteriorExiste = await _context.Editoras
            .AnyAsync(e => e.Id == dto.EditoraExteriorId && e.Origem == OrigemEditora.Exterior);

        if (!editoraExteriorExiste)
        {
            return BadRequest("Editora Exterior não Encontrada");
        }

        var editoraBrasilExiste = await _context.Editoras
            .AnyAsync(e => e.Id == dto.EditoraBrasilId && e.Origem == OrigemEditora.Brasil);

        if (!editoraBrasilExiste)
        {
            return BadRequest("Editora Brasil não Encontrada");
        }

        var statusExiste = await _context.Status
            .AnyAsync(e => e.Id == dto.StatusId);

        if (!statusExiste)
        {
            return BadRequest("Status não Encontrado");
        }

        itemExistente.Nome = dto.Nome;
        itemExistente.DataLancamento = dto.DataLancamento;
        itemExistente.EstadoId = dto.EstadoId;
        itemExistente.CodigoEAN = dto.CodigoEAN;
        itemExistente.DataAquisicao = dto.DataAquisicao;
        itemExistente.ValorAquisicao = dto.ValorAquisicao;
        itemExistente.FranquiaId = dto.FranquiaId;
        itemExistente.Observacoes = dto.Observacoes;
        leituraExistente.Tipo = dto.Tipo;
        leituraExistente.EditoraExteriorId = dto.EditoraExteriorId;
        leituraExistente.EditoraBrasilId = dto.EditoraBrasilId;
        leituraExistente.Autor = dto.Autor;
        leituraExistente.StatusId = dto.StatusId;
        leituraExistente.Lingua = dto.Lingua;
        leituraExistente.ISBN13 = dto.ISBN13;
        leituraExistente.Volume = dto.Volume;
        leituraExistente.VolumeAte = dto.VolumeAte;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Itens.FindAsync(id);
        var leitura = await _context.Leituras.FindAsync(id);

        if (item == null)
        {
            return NotFound("Item de Leitura não Encontrado");
        }

        if (leitura == null)
        {
            return NotFound("Leitura não Encontrado");
        }

        _context.Leituras.Remove(leitura);
        _context.Itens.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}