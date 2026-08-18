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
}