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
}