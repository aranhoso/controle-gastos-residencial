using CGR.Application.DTOs;

namespace CGR.Application.Interfaces;

/// <summary>
/// Define os contratos de caso de uso para gerenciamento de categorias na camada de Application.
/// </summary>
public interface ICategoriaService
{
    /// <summary>
    /// Cria uma nova categoria a partir dos dados informados.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação da categoria.</param>
    /// <returns>A categoria criada no formato de resposta.</returns>
    Task<CategoriaResponseDTO> CriarAsync(CategoriaRequestDTO dto);

    /// <summary>
    /// Atualiza uma categoria existente com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <param name="dto">Dados de entrada para atualização da categoria.</param>
    /// <returns>A categoria atualizada no formato de resposta.</returns>
    Task<CategoriaResponseDTO> AtualizarAsync(Guid id, CategoriaRequestDTO dto);

    /// <summary>
    /// Remove uma categoria pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>Uma tarefa assíncrona representando a operação de remoção.</returns>
    Task DeletarAsync(Guid id);

    /// <summary>
    /// Obtém uma categoria pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>A categoria encontrada ou <see langword="null"/> quando não existir.</returns>
    Task<CategoriaResponseDTO?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as categorias cadastradas.
    /// </summary>
    /// <returns>Uma coleção de categorias no formato de resposta.</returns>
    Task<IEnumerable<CategoriaResponseDTO>> ObterTodasAsync();
}