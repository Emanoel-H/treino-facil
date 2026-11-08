namespace Api.Models;

public class Exercicio
{
    public int Id {get; set; }
    public int TreinoId { get; set; }
    public Treino Treino { get; set; } = null!;
    public string Nome { get; set; } = "";
    public int Series { get; set; }
    public string? Repeticoes { get; set; }
    public int Ordem { get; set; }
}