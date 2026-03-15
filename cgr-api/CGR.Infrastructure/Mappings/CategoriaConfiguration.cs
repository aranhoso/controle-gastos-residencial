using CGR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CGR.Infrastructure.Mappings;

/// <summary>
/// Configura o mapeamento da entidade <see cref="Categoria"/> para o banco de dados utilizando o Fluent API do Entity Framework Core, definindo a estrutura da tabela, chaves primárias, propriedades e suas restrições, garantindo que os dados sejam armazenados de forma consistente e eficiente. Esta configuração é essencial para que o Entity Framework Core possa criar as tabelas corretas no banco de dados e aplicar as regras de validação definidas na entidade.
/// </summary>
public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnType("varchar(400)");

        builder.Property(c => c.Finalidade)
            .IsRequired();
    }
}
