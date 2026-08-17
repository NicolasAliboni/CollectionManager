using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EditorasController : ControllerBase
{
    private readonly AppDbContext _context;
    public EditorasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var editoras = await _context.Editoras.ToListAsync();

        return Ok(editoras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var editora = await _context.Editoras.FindAsync(id);

        if (editora == null)
        {
            return NotFound("Editora não Encontrada");
        }

        return Ok(editora);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Editora editora)
    {
        _context.Editoras.Add(editora);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),new { id = editora.Id },editora);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Editora editora, int id)
    {
        if(id != editora.Id)
        {
            return BadRequest("Os IDs não são compatíveis");
        }

        var editoraExistente = await _context.Editoras.FindAsync(id);
        
        if (editoraExistente == null)
        {
            return NotFound("Editora não Encontrada");
        }

        editoraExistente.Nome = editora.Nome;
        editoraExistente.Origem = editora.Origem;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var editora = await _context.Editoras.FindAsync(id);

        if (editora == null)
        {
            return NotFound("Editora não Encontrada");
        }

        _context.Editoras.Remove(editora);

        await _context.SaveChangesAsync();

        return Ok("Editora Excluida");
    }
}