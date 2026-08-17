using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly AppDbContext _context;
    public StatusController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var status = await _context.Status.ToListAsync();
        
        return Ok(status);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
    {
        var status = await _context.Status.FindAsync(id);

        if (status == null)
        {
            return NotFound("Status não Encontrado");
        }

        return Ok(status);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Status status)
    {
        _context.Status.Add(status);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Status status)
    {
        if(id != status.Id)
        {
            return BadRequest("Os IDs não são compatíveis");
        }

        var statusExistente = await _context.Status.FindAsync(id);

        if (statusExistente == null)
        {
            return NotFound("Status não Encontrado");
        }

        statusExistente.Nome = status.Nome;
        statusExistente.Tipo = status.Tipo;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var status = await _context.Status.FindAsync(id);

        if (status == null)
        {
            return NotFound("Status não Encontrado");
        }

        _context.Remove(status);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}