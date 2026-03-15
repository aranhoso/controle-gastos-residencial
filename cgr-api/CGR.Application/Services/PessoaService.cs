using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Entities;
using CGR.Domain.Exceptions;
using CGR.Domain.Interfaces;

namespace CGR.Application.Services;

/// <summary>
/// Implementa os casos de uso de pessoa na camada de Application,
/// orquestrando DTOs, entidades de domínio e persistência.
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="PessoaService"/>.
    /// </summary>
    /// <param name="pessoaRepository">Repositório responsável pela persistência de pessoas.</param>
    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    /// <summary>
    /// Cria uma nova pessoa, aplicando as validações do domínio no construtor da entidade.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação.</param>
    /// <returns>A pessoa criada no formato de resposta.</returns>
    public async Task<PessoaResponseDTO> CriarAsync(PessoaRequestDTO dto)
    {
        var pessoa = new Pessoa(dto.Nome, dto.Idade);

        await _pessoaRepository.InserirAsync(pessoa);

        return MapearParaResponseDTO(pessoa);
    }

    /// <summary>
    /// Atualiza uma pessoa existente, validando sua existência antes da alteração.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <param name="dto">Dados de entrada para atualização.</param>
    /// <returns>A pessoa atualizada no formato de resposta.</returns>
    /// <exception cref="NotFoundException">Lançada quando a pessoa não for encontrada.</exception>
    public async Task<PessoaResponseDTO> AtualizarAsync(Guid id, PessoaRequestDTO dto)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id)
            ?? throw new NotFoundException(nameof(Pessoa), id);

        pessoa.AtualizarDados(dto.Nome, dto.Idade);

        await _pessoaRepository.AtualizarAsync(pessoa);

        return MapearParaResponseDTO(pessoa);
    }

    /// <summary>
    /// Remove uma pessoa existente.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>Uma tarefa assíncrona representando a remoção.</returns>
    /// <exception cref="NotFoundException">Lançada quando a pessoa não for encontrada.</exception>
    public async Task DeletarAsync(Guid id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id)
            ?? throw new NotFoundException(nameof(Pessoa), id);

        await _pessoaRepository.DeletarAsync(id);
    }

    /// <summary>
    /// Obtém uma pessoa por identificador.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>A pessoa encontrada ou <see langword="null"/> quando não existir.</returns>
    public async Task<PessoaResponseDTO?> ObterPorIdAsync(Guid id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

        if (pessoa == null) return null;

        return MapearParaResponseDTO(pessoa);
    }

    /// <summary>
    /// Obtém todas as pessoas e projeta para DTO de resposta.
    /// </summary>
    /// <returns>Uma coleção de pessoas no formato de resposta.</returns>
    public async Task<IEnumerable<PessoaResponseDTO>> ObterTodasAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodasAsync();

        return pessoas.Select(MapearParaResponseDTO);
    }

    /// <summary>
    /// Converte a entidade de domínio <see cref="Pessoa"/> para <see cref="PessoaResponseDTO"/>.
    /// </summary>
    /// <param name="pessoa">Entidade de pessoa a ser convertida.</param>
    /// <returns>DTO de resposta correspondente.</returns>
    private static PessoaResponseDTO MapearParaResponseDTO(Pessoa pessoa)
    {
        return new PessoaResponseDTO
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }
}
