using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Entities;
using CGR.Domain.Exceptions;
using CGR.Domain.Interfaces;

namespace CGR.Application.Services;

/// <summary>
/// Implementa os casos de uso de categoria na camada de Application,
/// orquestrando DTOs, entidades de domínio e persistência.
/// </summary>
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CategoriaService"/>.
    /// </summary>
    /// <param name="categoriaRepository">Repositório responsável pela persistência de categorias.</param>
    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    /// <summary>
    /// Cria uma nova categoria, aplicando as validações do domínio no construtor da entidade.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação.</param>
    /// <returns>A categoria criada no formato de resposta.</returns>
    public async Task<CategoriaResponseDTO> CriarAsync(CategoriaRequestDTO dto)
    {
        var categoria = new Categoria(dto.Descricao, dto.Finalidade);

        await _categoriaRepository.InserirAsync(categoria);

        return MapearParaResponseDTO(categoria);
    }

    /// <summary>
    /// Atualiza uma categoria existente, validando sua existência antes da alteração.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <param name="dto">Dados de entrada para atualização.</param>
    /// <returns>A categoria atualizada no formato de resposta.</returns>
    /// <exception cref="NotFoundException">Lançada quando a categoria não for encontrada.</exception>
    public async Task<CategoriaResponseDTO> AtualizarAsync(Guid id, CategoriaRequestDTO dto)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id) 
            ?? throw new NotFoundException(nameof(Categoria), id);
            
        categoria.AtualizarDados(dto.Descricao, dto.Finalidade);

        await _categoriaRepository.AtualizarAsync(categoria);

        return MapearParaResponseDTO(categoria);
    }

    /// <summary>
    /// Remove uma categoria existente.
    /// </summary>
    /// <remarks>
    /// Neste projeto, a verificação prévia de existência foi mantida para deixar o fluxo explícito
    /// e facilitar o entendimento do passo a passo da operação.
    /// </remarks>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>Uma tarefa assíncrona representando a remoção.</returns>
    /// <exception cref="NotFoundException">Lançada quando a categoria não for encontrada.</exception>
    public async Task DeletarAsync(Guid id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id)
            ?? throw new NotFoundException(nameof(Categoria), id);

        await _categoriaRepository.DeletarAsync(id);
    }

    /// <summary>
    /// Obtém uma categoria por identificador.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>A categoria encontrada ou <see langword="null"/> quando não existir.</returns>
    public async Task<CategoriaResponseDTO?> ObterPorIdAsync(Guid id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        
        if (categoria == null) return null;

        return MapearParaResponseDTO(categoria);
    }

    /// <summary>
    /// Obtém todas as categorias e projeta para DTO de resposta.
    /// </summary>
    /// <returns>Uma coleção de categorias no formato de resposta.</returns>
    public async Task<IEnumerable<CategoriaResponseDTO>> ObterTodasAsync()
    {
        var categorias = await _categoriaRepository.ObterTodasAsync();
        
        return categorias.Select(MapearParaResponseDTO);
    }

            /// <summary>
            /// Converte a entidade de domínio <see cref="Categoria"/> para <see cref="CategoriaResponseDTO"/>.
            /// </summary>
            /// <param name="categoria">Entidade de categoria a ser convertida.</param>
            /// <returns>DTO de resposta correspondente.</returns>
    private static CategoriaResponseDTO MapearParaResponseDTO(Categoria categoria)
    {
        return new CategoriaResponseDTO
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade
        };
    }
}