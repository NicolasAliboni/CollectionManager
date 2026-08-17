using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var estados = await _context.Estados.ToListAsync();

        return Ok(estados);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estado = await _context.Estados.FindAsync(id);

        if (estado == null)
        {
            return NotFound();
        }

        return Ok(estado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Estado estado)
    {
        _context.Estados.Add(estado);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),new { id = estado.Id },estado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Estado estado)
    {
        if (id != estado.Id)
        {
            return BadRequest();
        }

        var estadoExistente = await _context.Estados.FindAsync(id);

        if (estadoExistente == null)
        {
            return NotFound();
        }

        estadoExistente.Nome = estado.Nome;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estado = await _context.Estados.FindAsync(id);

        if (estado == null)
        {
            return NotFound();
        }

        _context.Estados.Remove(estado);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}