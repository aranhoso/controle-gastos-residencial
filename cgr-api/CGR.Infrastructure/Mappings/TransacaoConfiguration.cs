using CGR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CGR.Infrastructure.Mappings;

/// <summary>
/// Configura o mapeamento da entidade <see cref="Transacao"/> para o banco de dados utilizando o Fluent API do Entity Framework Core, definindo a estrutura da tabela, chaves primárias, propriedades e suas restrições, garantindo que os dados sejam armazenados de forma consistente e eficiente. Esta configuração é essencial para que o Entity Framework Core possa criar as tabelas corretas no banco de dados e aplicar as regras de validação definidas na entidade.
/// </summary>
public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnType("varchar(400)");

        builder.Property(t => t.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");


        builder.Property(t => t.Tipo)
            .IsRequired()
            .HasColumnType("int");

        builder.HasOne(t => t.Pessoa)
            .WithMany()
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Categoria)
            .WithMany()
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}