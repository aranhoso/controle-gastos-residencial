using System.ComponentModel.DataAnnotations;
using CGR.Domain.Enums;

namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para receber os dados de criação ou atualização de uma Transação via API.
/// </summary>
public sealed class TransacaoRequestDTO
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(400, ErrorMessage = "A descrição não pode exceder 400 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "A pessoa é obrigatória.")]
    public Guid PessoaId { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public Guid CategoriaId { get; set; }
}
