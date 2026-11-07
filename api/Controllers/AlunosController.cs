using Api.Data;
using Api.Models;
using Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _db;
    public AlunosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> Listar([FromQuery] string? busca)
    {
        var query = _db.Alunos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
            query = query.Where(a => a.Nome.Contains(busca));

        return await query.OrderBy(a => a.Nome).ToListAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Aluno>> ObterPorId(int id)
    {
        var aluno = await _db.Alunos.FindAsync(id);
        return aluno is null ? NotFound() : Ok(aluno);
    }

    [HttpPost]
    public async Task<ActionResult<Aluno>> Criar(AlunoRequest request)
    {
        var aluno = new Aluno
        {
            Nome = request.Nome,
            Telefone = request.Telefone,
            Objetivo = request.Objetivo
        };

        _db.Alunos.Add(aluno);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = aluno.Id }, aluno);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, AlunoRequest request)
    {
        var aluno = await _db.Alunos.FindAsync(id);
        
        if (aluno is null)
            return NotFound();

        aluno.Nome = request.Nome;
        aluno.Telefone = request.Telefone;
        aluno.Objetivo = request.Objetivo;
        aluno.AlteradoEm = DateTime.UtcNow;
        
        await _db.SaveChangesAsync();
        return NoContent();
    } 
}