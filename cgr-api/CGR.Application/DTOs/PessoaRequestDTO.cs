using System.ComponentModel.DataAnnotations;

namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para receber os dados de criação ou atualização de uma Pessoa via API.
/// </summary>
public sealed class PessoaRequestDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "A idade não pode ser negativa.")]
    public int Idade { get; set; }
}
