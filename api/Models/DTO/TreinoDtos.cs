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
