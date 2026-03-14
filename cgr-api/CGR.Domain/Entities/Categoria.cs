using CGR.Domain.Enums;

namespace CGR.Domain.Entities;

public class Categoria
{
    // Id da categoria, gerado automaticamente
    public Guid Id { get; private set; }
    // Descrição da categoria, obrigatória (limite de 400 caracteres)
    public string Descricao { get; private set; } = string.Empty;
    // Finalidade da categoria, obrigatória e enumerada (Receita, Despesa ou Ambas)
    public FinalidadeCategoria Finalidade { get; private set; }

    // Construtor vazio necessário para o Entity Framework Core, pois ele precisa criar instâncias da classe para mapear os dados do banco de dados.
    protected Categoria() { }

    // Construtor para criar uma nova categoria, garantindo que os dados sejam validados e a entidade seja criada de forma consistente, seguindo os princípios de Rich Domain Model.
    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        ValidarDados(descricao, finalidade);
        Id = Guid.NewGuid();
        Descricao = descricao;
        Finalidade = finalidade;
    }

    // Método para permitir edição controlada, garantindo que os dados sejam validados antes de serem atualizados.
    public void AtualizarDados(string descricao, FinalidadeCategoria finalidade)
    {
        ValidarDados(descricao, finalidade);
        Descricao = descricao;
        Finalidade = finalidade;
    }

    // Método de validação de dados, garantindo o Rich Domain Model e evitando que a entidade seja criada ou atualizada com dados inválidos.
    private void ValidarDados(string descricao, FinalidadeCategoria finalidade)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.");
        if (!Enum.IsDefined(typeof(FinalidadeCategoria), finalidade))
            throw new ArgumentException("A finalidade de categoria inválida.");
    }
}
