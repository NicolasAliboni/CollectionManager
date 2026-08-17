using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FranquiasController : ControllerBase
{
    private readonly AppDbContext _context;

    public FranquiasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var franquias = await _context.Franquias.ToListAsync();

        return Ok(franquias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var franquia = await _context.Franquias.FindAsync(id);

        if (franquia == null)
        {
            return NotFound();
        }

        return Ok(franquia);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Franquia franquia)
    {
        _context.Franquias.Add(franquia);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = franquia.Id }, franquia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Franquia franquia)
    {
        if (id != franquia.Id)
        {
            return BadRequest();
        }

        var franquiaExistente = await _context.Franquias.FindAsync(id);

        if (franquiaExistente == null)
        {
            return NotFound();
        }

        franquiaExistente.Nome = franquia.Nome;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var franquia = await _context.Franquias.FindAsync(id);

        if (franquia == null)
        {
            return NotFound();
        }

        _context.Remove(franquia);

        await _context.SaveChangesAsync();

        return NoContent();
    }

}