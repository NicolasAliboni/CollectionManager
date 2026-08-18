using CollectionManager.Api.Data;
using CollectionManager.Api.DTOs;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideogamesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VideogamesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var videogame = await _context.Videogames
            .Include(v => v.Item)
                .ThenInclude(i => i.Estado)
            .Include(v => v.Item)
                .ThenInclude(i => i.Franquia)
            .Include(v => v.Marca)
            .ToListAsync();

        return Ok(videogame);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var videogame = await _context.Videogames
            .Include(v => v.Item)
                .ThenInclude(i => i.Estado)
            .Include(v => v.Item)
                .ThenInclude(i => i.Franquia)
            .Include(v => v.Marca)
            .FirstOrDefaultAsync(c => c.ItemId == id);

        if (videogame == null)
        {
            return NotFound("Videogame não Encontrado");
        }

        return Ok(videogame);
    }

    [HttpPost]
    public async Task<IActionResult> Post(VideogameCreateDto dto)
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
            .AnyAsync(e => e.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não Encontrada");
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

            var videogame = new Videogame
            {
                ItemId = item.Id,
                MarcaId = dto.MarcaId
            };

            _context.Videogames.Add(videogame);

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
    public async Task<IActionResult> Put(VideogameCreateDto dto, int id)
    {
        var itemExistente = await _context.Itens.FindAsync(id);
        var videogameExistente = await _context.Videogames.FindAsync(id);

        if (itemExistente == null)
        {
            return NotFound("Item do Videogame não Encontrado");
        }

        if (videogameExistente == null)
        {
            return NotFound("Videogame não Encontrado");
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
                .AnyAsync(f => f.Id == dto.FranquiaId.Value);
            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var marcaExiste = await _context.Marcas
            .AnyAsync(e => e.Id == dto.MarcaId);

        if (!marcaExiste)
        {
            return BadRequest("Marca não Encontrada");
        }

        itemExistente.Nome = dto.Nome;
        itemExistente.DataLancamento = dto.DataLancamento;
        itemExistente.EstadoId = dto.EstadoId;
        itemExistente.CodigoEAN = dto.CodigoEAN;
        itemExistente.DataAquisicao = dto.DataAquisicao;
        itemExistente.ValorAquisicao = dto.ValorAquisicao;
        itemExistente.FranquiaId = dto.FranquiaId;
        itemExistente.Observacoes = dto.Observacoes;
        videogameExistente.MarcaId = dto.MarcaId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Itens.FindAsync(id);
        var videogame = await _context.Videogames.FindAsync(id);

        if (item == null)
        {
            return NotFound("Item do Videogame não Encontrado");
        }

        if (videogame == null)
        {
            return NotFound("Videogame não Encontrado");
        }

        _context.Videogames.Remove(videogame);
        _context.Itens.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }

}