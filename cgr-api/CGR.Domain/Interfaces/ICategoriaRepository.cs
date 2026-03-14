using CGR.Domain.Entities;

namespace CGR.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de categorias, definindo os métodos para operações CRUD e consultas relacionadas a categorias.
/// </summary>
public interface ICategoriaRepository
{
    /// <summary>
    /// Insere uma nova categoria no repositório.
    /// </summary>
    /// <param name="categoria">A entidade categoria a ser inserida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task InserirAsync(Categoria categoria);

    /// <summary>
    /// Atualiza as informações de uma categoria existente no repositório.
    /// </summary>
    /// <param name="categoria">A entidade categoria a ser atualizada.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task AtualizarAsync(Categoria categoria);

    /// <summary>
    /// Remove uma categoria do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da categoria a ser removida.</param>
    /// <returns>Uma tarefa assíncrona representando a operação.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma categoria do repositório com base no seu identificador único (ID).
    /// </summary>
    /// <param name="id">O identificador único da categoria a ser obtida.</param>
    /// <returns>Uma tarefa assíncrona que retorna a categoria encontrada ou nula se não existir.</returns>
    Task<Categoria?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as categorias do repositório.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que retorna uma coleção de categorias.</returns>
    Task<IEnumerable<Categoria>> ObterTodasAsync();
}