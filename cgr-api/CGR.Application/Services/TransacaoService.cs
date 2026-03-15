using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Entities;
using CGR.Domain.Exceptions;
using CGR.Domain.Interfaces;

namespace CGR.Application.Services;

/// <summary>
/// Implementa os casos de uso de transação na camada de Application,
/// orquestrando DTOs, entidades de domínio e persistência.
/// </summary>
public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="TransacaoService"/>.
    /// </summary>
    /// <param name="transacaoRepository">Repositório responsável pela persistência de transações.</param>
    /// <param name="pessoaRepository">Repositório para consulta da pessoa vinculada.</param>
    /// <param name="categoriaRepository">Repositório para consulta da categoria vinculada.</param>
    public TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository,
        ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
        _categoriaRepository = categoriaRepository;
    }

    /// <summary>
    /// Cria uma nova transação, resolvendo pessoa e categoria por ID antes de montar a entidade de domínio.
    /// </summary>
    /// <param name="dto">Dados de entrada para criação.</param>
    /// <returns>A transação criada no formato de resposta.</returns>
    /// <exception cref="NotFoundException">Lançada quando pessoa ou categoria não forem encontradas.</exception>
    public async Task<TransacaoResponseDTO> CriarAsync(TransacaoRequestDTO dto)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId)
            ?? throw new NotFoundException(nameof(Pessoa), dto.PessoaId);

        var categoria = await _categoriaRepository.ObterPorIdAsync(dto.CategoriaId)
            ?? throw new NotFoundException(nameof(Categoria), dto.CategoriaId);

        var transacao = new Transacao(dto.Descricao, dto.Valor, dto.Tipo, pessoa, categoria);

        await _transacaoRepository.InserirAsync(transacao);

        return MapearParaResponseDTO(transacao, pessoa.Nome, categoria.Descricao);
    }

    /// <summary>
    /// Atualiza uma transação existente, validando sua existência e resolvendo pessoa e categoria por ID.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <param name="dto">Dados de entrada para atualização.</param>
    /// <returns>A transação atualizada no formato de resposta.</returns>
    /// <exception cref="NotFoundException">Lançada quando transação, pessoa ou categoria não forem encontradas.</exception>
    public async Task<TransacaoResponseDTO> AtualizarAsync(Guid id, TransacaoRequestDTO dto)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id)
            ?? throw new NotFoundException(nameof(Transacao), id);

        var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId)
            ?? throw new NotFoundException(nameof(Pessoa), dto.PessoaId);

        var categoria = await _categoriaRepository.ObterPorIdAsync(dto.CategoriaId)
            ?? throw new NotFoundException(nameof(Categoria), dto.CategoriaId);

        transacao.AtualizarDados(dto.Descricao, dto.Valor, dto.Tipo, pessoa, categoria);

        await _transacaoRepository.AtualizarAsync(transacao);

        return MapearParaResponseDTO(transacao, pessoa.Nome, categoria.Descricao);
    }

    /// <summary>
    /// Remove uma transação existente.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>Uma tarefa assíncrona representando a remoção.</returns>
    /// <exception cref="NotFoundException">Lançada quando a transação não for encontrada.</exception>
    public async Task DeletarAsync(Guid id)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id)
            ?? throw new NotFoundException(nameof(Transacao), id);

        await _transacaoRepository.DeletarAsync(id);
    }

    /// <summary>
    /// Obtém uma transação por identificador.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>A transação encontrada ou <see langword="null"/> quando não existir.</returns>
    public async Task<TransacaoResponseDTO?> ObterPorIdAsync(Guid id)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id);

        if (transacao == null) return null;

        return MapearParaResponseDTO(transacao);
    }

    /// <summary>
    /// Obtém todas as transações e projeta para DTO de resposta.
    /// </summary>
    /// <returns>Uma coleção de transações no formato de resposta.</returns>
    public async Task<IEnumerable<TransacaoResponseDTO>> ObterTodasAsync()
    {
        var transacoes = await _transacaoRepository.ObterTodasAsync();

        return transacoes.Select(t => MapearParaResponseDTO(t));
    }

    /// <summary>
    /// Converte a entidade de domínio <see cref="Transacao"/> para <see cref="TransacaoResponseDTO"/>.
    /// </summary>
    /// <param name="transacao">Entidade de transação a ser convertida.</param>
    /// <param name="nomePessoa">Nome da pessoa associada, quando já conhecido no fluxo.</param>
    /// <param name="descricaoCategoria">Descrição da categoria associada, quando já conhecida no fluxo.</param>
    /// <returns>DTO de resposta correspondente.</returns>
    private static TransacaoResponseDTO MapearParaResponseDTO(
        Transacao transacao,
        string? nomePessoa = null,
        string? descricaoCategoria = null)
    {
        return new TransacaoResponseDTO
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            PessoaId = transacao.PessoaId,
            NomePessoa = nomePessoa ?? transacao.Pessoa?.Nome ?? string.Empty,
            CategoriaId = transacao.CategoriaId,
            DescricaoCategoria = descricaoCategoria ?? transacao.Categoria?.Descricao ?? string.Empty
        };
    }
}
