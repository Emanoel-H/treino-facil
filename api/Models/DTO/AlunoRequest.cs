using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO;

public class AlunoRequest
{
    [Required, MaxLength(120)]
    public string Nome { get; set; } = "";
    
    [MaxLength(20)]
    public string? Telefone { get; set; }
    
    [MaxLength(200)]
    public string? Objetivo { get; set; }
}