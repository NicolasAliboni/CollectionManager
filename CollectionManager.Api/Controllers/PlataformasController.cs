using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PlataformasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlataformasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var plataforma = await _context.Plataformas.ToListAsync();

        return Ok(plataforma);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var plataforma = await _context.Plataformas.FindAsync(id);

        if (plataforma == null)
        {
            return NotFound("O ID não foi encontrado");
        }

        return Ok(plataforma);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Plataforma plataforma)
    {
        _context.Plataformas.Add(plataforma);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = plataforma.Id }, plataforma);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Plataforma plataforma)
    {
        if (id != plataforma.Id)
        {
            return BadRequest("Os IDs não são compatíveis");
        }

        var plataformaExistente = await _context.Plataformas.FindAsync(id);

        if (plataformaExistente == null)
        {
            return NotFound("O ID não foi encontrado");
        }

        plataformaExistente.Nome = plataforma.Nome;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plataforma = await _context.Plataformas.FindAsync(id);

        if (plataforma == null)
        {
            return NotFound("O ID não foi encontrado");
        }

        _context.Remove(plataforma);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}