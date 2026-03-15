using CGR.Domain.Entities;
using CGR.Domain.Interfaces;
using CGR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CGR.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório responsável pela persistência e consulta de entidades <see cref="Transacao"/>.
/// </summary>
public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="TransacaoRepository"/>.
    /// </summary>
    /// <param name="context">Contexto de banco de dados utilizado para operações com transações.</param>
    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Insere uma nova transação no banco de dados.
    /// </summary>
    /// <param name="transacao">Entidade de transação a ser persistida.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de inserção.</returns>
    public async Task InserirAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Atualiza os dados de uma transação existente no banco de dados.
    /// </summary>
    /// <param name="transacao">Entidade de transação com os dados atualizados.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de atualização.</returns>
    public async Task AtualizarAsync(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove uma transação do banco de dados com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de remoção.</returns>
    public async Task DeletarAsync(Guid id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);

        if (transacao != null)
        {
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Obtém uma transação pelo seu identificador único, incluindo os relacionamentos de pessoa e categoria.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>
    /// A transação encontrada, ou <see langword="null"/> quando não existir registro para o identificador informado.
    /// </returns>
    public async Task<Transacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Obtém todas as transações cadastradas, incluindo os relacionamentos de pessoa e categoria.
    /// </summary>
    /// <returns>Uma coleção com todas as transações persistidas no banco de dados.</returns>
    public async Task<IEnumerable<Transacao>> ObterTodasAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .AsNoTracking()
            .ToListAsync();
    }
}