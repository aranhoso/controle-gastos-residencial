using CGR.Domain.Entities;
using CGR.Domain.Interfaces;
using CGR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CGR.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório responsável pela persistência e consulta de entidades <see cref="Categoria"/>.
/// </summary>
public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CategoriaRepository"/>.
    /// </summary>
    /// <param name="context">Contexto de banco de dados utilizado para operações com categorias.</param>
    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Insere uma nova categoria no banco de dados.
    /// </summary>
    /// <param name="categoria">Entidade de categoria a ser persistida.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de inserção.</returns>
    public async Task InserirAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Atualiza os dados de uma categoria existente no banco de dados.
    /// </summary>
    /// <param name="categoria">Entidade de categoria com os dados atualizados.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de atualização.</returns>
    public async Task AtualizarAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove uma categoria do banco de dados com base no identificador informado.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>Uma tarefa assíncrona que representa a operação de remoção.</returns>
    public async Task DeletarAsync(Guid id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Obtém uma categoria pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>
    /// A categoria encontrada, ou <see langword="null"/> quando não existir registro para o identificador informado.
    /// </returns>
    public async Task<Categoria?> ObterPorIdAsync(Guid id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    /// <summary>
    /// Obtém todas as categorias cadastradas.
    /// </summary>
    /// <returns>Uma coleção com todas as categorias persistidas no banco de dados.</returns>
    public async Task<IEnumerable<Categoria>> ObterTodasAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }
}