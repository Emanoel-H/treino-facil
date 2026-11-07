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

    
}