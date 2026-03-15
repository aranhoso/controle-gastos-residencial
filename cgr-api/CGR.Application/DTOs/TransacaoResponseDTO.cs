using CGR.Domain.Enums;

namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para retornar os dados de uma Transação para o front-end (React),
/// omitindo informações sensíveis ou desnecessárias do banco de dados.
/// </summary>
public sealed class TransacaoResponseDTO
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public string TipoDescricao => Tipo.ToString();
    public Guid PessoaId { get; set; }
    public Guid CategoriaId { get; set; }
}
