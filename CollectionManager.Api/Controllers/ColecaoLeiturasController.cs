using CollectionManager.Api.Data;
using CollectionManager.Api.DTOs;
using CollectionManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ColecaoLeiturasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColecaoLeiturasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var colecaoLeituras = await _context.ColecaoLeitura
            .Include(l => l.Franquia)
            .Include(l => l.EditoraExterior)
            .Include(l => l.EditoraBrasil)
            .ToListAsync();

        return Ok(colecaoLeituras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var colecaoLeituras = await _context.ColecaoLeitura
            .Include(l => l.Franquia)
            .Include(l => l.EditoraExterior)
            .Include(l => l.EditoraBrasil)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (colecaoLeituras == null)
        {
            return NotFound("Coleção não Encontrada");
        }

        return Ok(colecaoLeituras);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ColecaoLeituraCreateDto dto)
    {
        if (!Enum.IsDefined(typeof(TipoLeitura), dto.Tipo))
        {
            return BadRequest("Tipo de leitura inválido.");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias
            .AnyAsync(e => e.Id == dto.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        var editoraExteriorExiste = await _context.Editoras
           .AnyAsync(e => e.Id == dto.EditoraExteriorId && e.Origem == OrigemEditora.Exterior);

        if (!editoraExteriorExiste)
        {
            return BadRequest("Editora Exterior não Encontrada");
        }

        var editoraBrasilExiste = await _context.Editoras
            .AnyAsync(e => e.Id == dto.EditoraBrasilId && e.Origem == OrigemEditora.Brasil);

        if (!editoraBrasilExiste)
        {
            return BadRequest("Editora Brasil não Encontrada");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var colecaoLeitura = new ColecaoLeitura
            {
                Nome = dto.Nome,
                Tipo = dto.Tipo,
                FranquiaId = dto.FranquiaId,
                EditoraExteriorId = dto.EditoraExteriorId,
                EditoraBrasilId = dto.EditoraBrasilId,
                Autor = dto.Autor,
                Lingua = dto.Lingua,
                VolumeAte = dto.VolumeAte,
                Observacoes = dto.Observacoes
            };

            _context.ColecaoLeitura.Add(colecaoLeitura);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = colecaoLeitura.Id }, new
            {
                colecaoLeitura.Id,
                colecaoLeitura.Nome,
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
    public async Task<IActionResult> Put(int id, ColecaoLeituraCreateDto dto)
    {
        var colecaoLeituraExistente = await _context.ColecaoLeitura.FindAsync(id);

        if (colecaoLeituraExistente == null)
        {
            return NotFound("Coleção não Encontrada");
        }

        if (dto.FranquiaId.HasValue)
        {
            var franquiaExiste = await _context.Franquias
            .AnyAsync(e => e.Id == dto.FranquiaId.Value);

            if (!franquiaExiste)
            {
                return BadRequest("Franquia não Encontrada");
            }
        }

        if (!Enum.IsDefined(typeof(TipoLeitura), dto.Tipo))
        {
            return BadRequest("Tipo de leitura inválido.");
        }

        var editoraExteriorExiste = await _context.Editoras
           .AnyAsync(e => e.Id == dto.EditoraExteriorId && e.Origem == OrigemEditora.Exterior);

        if (!editoraExteriorExiste)
        {
            return BadRequest("Editora Exterior não Encontrada");
        }

        var editoraBrasilExiste = await _context.Editoras
            .AnyAsync(e => e.Id == dto.EditoraBrasilId && e.Origem == OrigemEditora.Brasil);

        if (!editoraBrasilExiste)
        {
            return BadRequest("Editora Brasil não Encontrada");
        }

        colecaoLeituraExistente.Nome = dto.Nome;
        colecaoLeituraExistente.Tipo = dto.Tipo;
        colecaoLeituraExistente.FranquiaId = dto.FranquiaId;
        colecaoLeituraExistente.EditoraExteriorId = dto.EditoraExteriorId;
        colecaoLeituraExistente.EditoraBrasilId = dto.EditoraBrasilId;
        colecaoLeituraExistente.Autor = dto.Autor;
        colecaoLeituraExistente.Lingua = dto.Lingua;
        colecaoLeituraExistente.VolumeAte = dto.VolumeAte;
        colecaoLeituraExistente.Observacoes = dto.Observacoes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var colecaoLeitura = await _context.ColecaoLeitura.FindAsync(id);

        if (colecaoLeitura == null)
        {
            return NotFound("Coleção não Encontrada");
        }

        _context.ColecaoLeitura.Remove(colecaoLeitura);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}