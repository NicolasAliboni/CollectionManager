using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MarcasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MarcasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var marca = await _context.Marcas.ToListAsync();

        return Ok(marca);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);

        if (marca == null)
        {
            return NotFound();
        }

        return Ok(marca);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Marca marca)
    {
        _context.Marcas.Add(marca);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = marca.Id }, marca);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Marca marca)
    {
        if (id != marca.Id)
        {
            return BadRequest();
        }

        var marcaExistente = await _context.Marcas.FindAsync(id);

        if (marcaExistente == null)
        {
            return NotFound();
        }

        marcaExistente.Nome = marca.Nome;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);

        if (marca == null)
        {
            return NotFound();
        }

        _context.Remove(marca);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}