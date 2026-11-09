using Api.Data;
using Api.Models;
using Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreinosController : ControllerBase
{
    private readonly AppDbContext _db;
    public TreinosController(AppDbContext db) => _db = db;

    private static TreinoResponse ToResponse(Treino t) => new(
    t.Id, t.Nome, t.AlunoId, t.Aluno.Nome, t.Descricao,
    t.Exercicios.OrderBy(x => x.Ordem)
        .Select(x => new ExercicioResponse(x.Id, x.Nome, x.Series, x.Repeticoes, x.Ordem))
        .ToList());

    private static Exercicio ToEntity(ExercicioRequest request) => new()
    {
        Nome = request.Nome, Series = request.Series, Repeticoes = request.Repeticoes, Ordem = request.Ordem
    };

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoResponse>>> Listar([FromQuery] int? alunoId)
    {
        var query = _db.Treinos.AsNoTracking()
            .Include(t => t.Aluno)
            .Include(t => t.Exercicios)
            .AsQueryable();

        if (alunoId.HasValue)
            query = query.Where(t => t.AlunoId == alunoId);

        var treinos = await query.OrderBy(t => t.Nome).ToListAsync();
        return treinos.Select(ToResponse).ToList();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TreinoResponse>> ObterPorId(int id)
    {
        var treino = await _db.Treinos.AsNoTracking()
            .Include(t => t.Aluno)
            .Include(t => t.Exercicios)
            .FirstOrDefaultAsync(t => t.Id == id);
        
        return treino is null ? NotFound() : ToResponse(treino);
    }
    
    
}