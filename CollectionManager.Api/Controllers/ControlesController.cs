using CollectionManager.Api.Data;
using CollectionManager.Api.DTOs;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ControlesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ControlesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var controle = await _context.Controles
            .Include(c => c.Item)
                .ThenInclude(i => i.Estado)
            .Include(c => c.Item)
                .ThenInclude(i => i.Franquia)
            .Include(c => c.Marca)
            .Include(c => c.Plataforma)
            .FirstOrDefaultAsync(c => c.ItemId == id);

        if (controle == null)
        {
            return NotFound("Controle não encontrado");
        }

        return Ok(controle);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ControleCreateDto dto)
    {
        var estadoExiste = await _context.Estados
            .AnyAsync(e => e.Id == dto.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não encontrado");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias
                .AnyAsync(f => f.Id == dto.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não encontrada");
            }
        }

        var marcaExiste = await _context.Marcas
            .AnyAsync(m => m.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não encontrada");
        }

        var plataformaExiste = await _context.Plataformas
            .AnyAsync(p => p.Id == dto.PlataformaId);

        if (!plataformaExiste)
        {
            return BadRequest("Plataforma não encontrada");
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

            var controle = new Controle
            {
                ItemId = item.Id,
                Modelo = dto.Modelo,
                MarcaId = dto.MarcaId,
                PlataformaId = dto.PlataformaId
            };

            _context.Controles.Add(controle);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById),new { id = item.Id },new
                {
                    item.Id,
                    item.Nome,
                    controle.Modelo
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
    public async Task<IActionResult> Put(int id, ControleCreateDto dto)
    {
        var itemExistente = await _context.Itens.FindAsync(id);
        var controleExistente = await _context.Controles.FindAsync(id);

        if (itemExistente == null)
        {
            return NotFound("Item do controle não encontrado");
        }

        if (controleExistente == null)
        {
            return NotFound("Controle não encontrado");
        }

        var estadoExiste = await _context.Estados.AnyAsync(e => e.Id == dto.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não encontrado");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias.AnyAsync(f => f.Id == dto.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não encontrada");
            }
        }

        var marcaExiste = await _context.Marcas
            .AnyAsync(m => m.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não encontrada");
        }

        var plataformaExiste = await _context.Plataformas
            .AnyAsync(p => p.Id == dto.PlataformaId);

        if (!plataformaExiste)
        {
            return BadRequest("Plataforma não encontrada");
        }

            itemExistente.Nome = dto.Nome;
            itemExistente.DataLancamento = dto.DataLancamento;
            itemExistente.EstadoId = dto.EstadoId;
            itemExistente.CodigoEAN = dto.CodigoEAN;
            itemExistente.DataAquisicao = dto.DataAquisicao;
            itemExistente.ValorAquisicao = dto.ValorAquisicao;
            itemExistente.FranquiaId = dto.FranquiaId;
            itemExistente.Observacoes = dto.Observacoes;
            controleExistente.Modelo = dto.Modelo;
            controleExistente.MarcaId = dto.MarcaId;
            controleExistente.PlataformaId = dto.PlataformaId;

            await _context.SaveChangesAsync();

            return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Itens.FindAsync(id);
        var controle = await _context.Controles.FindAsync(id);

        if (item == null)
        {
            return NotFound("Item do controle não encontrado");
        }

        if (controle == null)
        {
            return NotFound("controle não encontrado");
        }

        _context.Controles.Remove(controle);
        _context.Itens.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}