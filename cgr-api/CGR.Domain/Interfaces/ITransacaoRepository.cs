using CGR.Domain.Entities;

namespace CGR.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de transações, definindo os métodos para operações CRUD e consultas relacionadas a transações.
/// </summary>
public interface ITransacaoRepository
{
    /// <summary>
    /// Insere uma nova transação no repositório.
    /// </summary>
    /// <param name="transacao">A entidade transação a ser inserida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task InserirAsync(Transacao transacao);

    /// <summary>
    /// Atualiza uma transação existente no repositório.
    /// </summary>
    /// <param name="transacao">A entidade transação a ser atualizada.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task AtualizarAsync(Transacao transacao);

    /// <summary>
    /// Remove uma transação do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da transação a ser removida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma transação do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da transação a ser obtida.</param>
    /// <returns>Uma tarefa assíncrona que retorna a transação encontrada ou nula se não existir.</returns>
    Task<Transacao?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as transações do repositório.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que retorna uma coleção de transações.</returns>
    Task<IEnumerable<Transacao>> ObterTodasAsync();
}