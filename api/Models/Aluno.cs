namespace Api.Models;

public class Aluno
{
    public int Id {get; set; }
    public string Nome { get; set; } = "";
    public string? Telefone { get; set; }
    public string? Objetivo { get; set; }
    public DateTime CriadoEm { get; set; } =  DateTime.UtcNow;
    public DateTime AlteradoEm { get; set; } =  DateTime.UtcNow;
}