using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensController : ControllerBase
{
    private readonly AppDbContext _context;

    public ItensController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var itens = await _context.Itens
            .Include(i => i.Estado)
            .Include(i => i.Franquia)
            .ToListAsync();

        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _context.Itens
            .Include(i => i.Estado)
            .Include(i => i.Franquia)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return NotFound("Item não Encontrado");
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Item item)
    {
        var estadoExiste = await _context.Estados.AnyAsync(e => e.Id == item.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não encontrado");
        }

        if (item.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias.AnyAsync(f => f.Id == item.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não encontrada");
            }
        }

        _context.Itens.Add(item);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),new { id = item.Id },item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Item item)
    {
        if (id != item.Id)
        {
            return BadRequest("Os IDs não são compatíveis");
        }

        var itemExistente = await _context.Itens.FindAsync(id);

        if (itemExistente == null)
        {
            return NotFound("Item não encontrado");
        }

        var estadoExiste = await _context.Estados.AnyAsync(e => e.Id == item.EstadoId);

        if (!estadoExiste)
        {
            return BadRequest("Estado não encontrado");
        }

        if (item.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias.AnyAsync(f => f.Id == item.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não encontrada");
            }
        }

        itemExistente.Nome = item.Nome;
        itemExistente.DataLancamento = item.DataLancamento;
        itemExistente.EstadoId = item.EstadoId;
        itemExistente.CodigoEAN = item.CodigoEAN;
        itemExistente.DataAquisicao = item.DataAquisicao;
        itemExistente.ValorAquisicao = item.ValorAquisicao;
        itemExistente.FranquiaId = item.FranquiaId;
        itemExistente.Observacoes = item.Observacoes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Itens.FindAsync(id);

        if (item == null)
        {
            return NotFound("Item não encontrado");
        }

        _context.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}