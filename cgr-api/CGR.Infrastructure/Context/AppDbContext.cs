using System.Reflection;
using CGR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CGR.Infrastructure.Context;

/// <summary>
/// Representa o contexto do banco de dados da aplicação, responsável por gerenciar as entidades e suas configurações. Utiliza o Entity Framework Core para mapear as classes de domínio para as tabelas do banco de dados, permitindo a realização de operações de CRUD (Create, Read, Update, Delete) e consultas complexas de forma eficiente e segura. O contexto é configurado para aplicar as regras de negócio definidas nas entidades e garantir a integridade dos dados armazenados.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do contexto do banco de dados com as opções fornecidas, permitindo a configuração da conexão com o banco de dados e outras opções relacionadas ao comportamento do Entity Framework Core. Este construtor é essencial para que o contexto possa ser configurado corretamente durante a inicialização da aplicação, garantindo que as operações de acesso a dados sejam realizadas de forma eficiente e segura.
     /// </summary>
     /// <param name="options">As opções de configuração do contexto, incluindo a string de conexão e outras configurações relacionadas ao comportamento do Entity Framework Core.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    /// <summary>
    /// Configura o modelo do banco de dados aplicando as configurações definidas nas classes de mapeamento utilizando o Fluent API do Entity Framework Core. Este método é chamado durante a inicialização do contexto para garantir que as entidades sejam mapeadas corretamente para as tabelas do banco de dados, aplicando as regras de validação e restrições definidas nas entidades. A aplicação das configurações é essencial para garantir a integridade dos dados e o desempenho das operações de acesso a dados realizadas pela aplicação.
    /// </summary>
    /// <param name="modelBuilder">O construtor de modelo utilizado para configurar as entidades e suas relações no banco de dados.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}