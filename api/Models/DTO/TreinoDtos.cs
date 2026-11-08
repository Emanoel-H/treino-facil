using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO;

public class ExercicioRequest
{
    [Required, MaxLength(120)]
    public string Nome { get; set; } = "";

    [Range(1, 20)] 
    public int Series;
    
    [MaxLength(60)]
    public string? Repeticoes { get; set; }
    
    [Range(1, 20)] 
    public int Ordem;
}
public class TreinoRequest
{
    [Range(1, int.MaxValue)]
    public int AlunoId { get; set; }
    
    [Required, MaxLength(120)]
    public string Nome { get; set; } = "";
    
    [MaxLength(300)]
    public string? Descricao { get; set; }

    public List<ExercicioRequest> Exercicios { get; set; } = new();
}

public record ExercicioResponse(int Id, string Nome, int Series, string? Repeticoes, int Ordem);
