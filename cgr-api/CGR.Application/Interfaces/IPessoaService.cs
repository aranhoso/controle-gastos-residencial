using CGR.Application.DTOs;

namespace CGR.Application.Interfaces;

/// <summary>
/// Define os contratos de caso de uso para gerenciamento de pessoas na camada de Application.
/// </summary>
public interface IPessoaService
{
    /// <summary>
    /// Cria uma nova pessoa a partir dos dados informados.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação da pessoa.</param>
    /// <returns>A pessoa criada no formato de resposta.</returns>
    Task<PessoaResponseDTO> CriarAsync(PessoaRequestDTO dto);

    /// <summary>
    /// Atualiza uma pessoa existente com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <param name="dto">Dados de entrada para atualização da pessoa.</param>
    /// <returns>A pessoa atualizada no formato de resposta.</returns>
    Task<PessoaResponseDTO> AtualizarAsync(Guid id, PessoaRequestDTO dto);

    /// <summary>
    /// Remove uma pessoa pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>Uma tarefa assíncrona representando a operação de remoção.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma pessoa pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>A pessoa encontrada ou <see langword="null"/> quando não existir.</returns>
    Task<PessoaResponseDTO?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Uma coleção de pessoas no formato de resposta.</returns>
    Task<IEnumerable<PessoaResponseDTO>> ObterTodasAsync();
}
