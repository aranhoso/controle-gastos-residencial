using CGR.Domain.Enums;

namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para retornar os dados de uma Categoria para o front-end (React),
/// omitindo informações sensíveis ou desnecessárias do banco de dados.
/// </summary>
public sealed class CategoriaResponseDTO
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
    public string FinalidadeDescricao => Finalidade.ToString(); 
}