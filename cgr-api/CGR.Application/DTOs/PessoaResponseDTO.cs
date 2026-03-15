namespace CGR.Application.DTOs;

/// <summary>
/// Objeto utilizado para retornar os dados de uma Pessoa para o front-end (React),
/// omitindo informações sensíveis ou desnecessárias do banco de dados.
/// </summary>
public sealed class PessoaResponseDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}
