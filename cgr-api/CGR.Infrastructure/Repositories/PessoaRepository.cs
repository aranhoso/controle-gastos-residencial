using CGR.Domain.Entities;
using CGR.Domain.Interfaces;
using CGR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CGR.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório responsável pela persistência e consulta de entidades <see cref="Pessoa"/>.
/// </summary>
public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="PessoaRepository"/>.
    /// </summary>
    /// <param name="context">Contexto de banco de dados utilizado para operações com pessoas.</param>
    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Insere uma nova pessoa no banco de dados.
    /// </summary>
    /// <param name="pessoa">Entidade de pessoa a ser persistida.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de inserção.</returns>
    public async Task InserirAsync(Pessoa pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync(); // É isso que efetiva a gravação no banco de dados
    }

    /// <summary>
    /// Atualiza os dados de uma pessoa existente no banco de dados.
    /// </summary>
    /// <param name="pessoa">Entidade de pessoa com os dados atualizados.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de atualização.</returns>
    public async Task AtualizarAsync(Pessoa pessoa)
    {
        _context.Pessoas.Update(pessoa);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove uma pessoa do banco de dados com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de remoção.</returns>
    public async Task DeletarAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Obtém uma pessoa pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>
    /// A pessoa encontrada, ou <see langword="null"/> quando não existir registro para o identificador informado.
    /// </returns>
    public async Task<Pessoa?> ObterPorIdAsync(Guid id)
    {
        // O FindAsync é a forma mais rápida do EF Core de buscar pela Chave Primária
        return await _context.Pessoas.FindAsync(id);
    }

    /// <summary>
    /// Obtém todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Uma coleção com todas as pessoas persistidas no banco de dados.</returns>
    public async Task<IEnumerable<Pessoa>> ObterTodasAsync()
    {
        // Dica de Sênior: O AsNoTracking() diz ao EF Core para não "ficar vigiando" essa lista,
        // o que deixa a consulta de leitura muuuito mais rápida e consome menos memória RAM.
        return await _context.Pessoas.AsNoTracking().ToListAsync();
    }
}