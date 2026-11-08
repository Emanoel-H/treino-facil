namespace Api.Models;

public class Treino
{
    public int Id {get; set; }
    public int AlunoId {get; set; }
    public Aluno Aluno { get; set; } = null!;
    public string Nome { get; set; } = "";
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; } =  DateTime.UtcNow;
    public DateTime AlteradoEm { get; set; } =  DateTime.UtcNow;
    public List<Exercicio> Exercicios { get; set; } = new();
}