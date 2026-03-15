using CGR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CGR.Infrastructure.Mappings;

/// <summary>
/// Configura o mapeamento da entidade <see cref="Pessoa"/> para o banco de dados utilizando o Fluent API do Entity Framework Core, definindo a estrutura da tabela, chaves primárias, propriedades e suas restrições, garantindo que os dados sejam armazenados de forma consistente e eficiente. Esta configuração é essencial para que o Entity Framework Core possa criar as tabelas corretas no banco de dados e aplicar as regras de validação definidas na entidade.
/// </summary>
public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(p => p.Idade)
            .IsRequired();
    }
}
