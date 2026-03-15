using CGR.Application.DTOs;

namespace CGR.Application.Interfaces;

/// <summary>
/// Define os contratos de caso de uso para gerenciamento de transações na camada de Application.
/// </summary>
public interface ITransacaoService
{
    /// <summary>
    /// Cria uma nova transação a partir dos dados informados.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação da transação.</param>
    /// <returns>A transação criada no formato de resposta.</returns>
    Task<TransacaoResponseDTO> CriarAsync(TransacaoRequestDTO dto);

    /// <summary>
    /// Atualiza uma transação existente com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <param name="dto">Dados de entrada para atualização da transação.</param>
    /// <returns>A transação atualizada no formato de resposta.</returns>
    Task<TransacaoResponseDTO> AtualizarAsync(Guid id, TransacaoRequestDTO dto);

    /// <summary>
    /// Remove uma transação pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>Uma tarefa assíncrona representando a operação de remoção.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma transação pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>A transação encontrada ou <see langword="null"/> quando não existir.</returns>
    Task<TransacaoResponseDTO?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as transações cadastradas.
    /// </summary>
    /// <returns>Uma coleção de transações no formato de resposta.</returns>
    Task<IEnumerable<TransacaoResponseDTO>> ObterTodasAsync();
}
