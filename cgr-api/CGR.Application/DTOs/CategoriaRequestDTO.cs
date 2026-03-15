using System.ComponentModel.DataAnnotations;
using CGR.Domain.Enums;

namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para receber os dados de criação ou atualização de uma Categoria via API.
/// </summary>
public sealed class CategoriaRequestDTO
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(400, ErrorMessage = "A descrição não pode exceder 400 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A finalidade da categoria é obrigatória.")]
    public FinalidadeCategoria Finalidade { get; set; }
}