using CGR.Domain.Entities;

namespace CGR.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de pessoas, definindo os métodos para operações CRUD e consultas relacionadas a pessoas.
/// </summary>
public interface IPessoaRepository
{
    /// <summary>
    /// Insere uma nova pessoa no repositório.
    /// </summary>
    /// <param name="pessoa">A entidade pessoa a ser inserida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task InserirAsync(Pessoa pessoa);

    /// <summary>
    /// Atualiza as informações de uma pessoa existente no repositório.
    /// </summary>
    /// <param name="pessoa">A entidade pessoa a ser atualizada.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task AtualizarAsync(Pessoa pessoa);
    
    /// <summary>
    /// Remove uma pessoa do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da pessoa a ser removida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma pessoa do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da pessoa a ser obtida.</param>
    /// <returns>Uma tarefa assíncrona que retorna a pessoa encontrada ou nula se não existir.</returns>
    Task<Pessoa?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as pessoas do repositório.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que retorna uma coleção de pessoas.</returns>
    Task<IEnumerable<Pessoa>> ObterTodasAsync();
}