namespace CGR.Domain.Entities;

public class Pessoa
{
    // Id da pessoa, gerado automaticamente
    public Guid Id { get; private set; }
    // Nome da pessoa, obrigatório
    public string Nome { get; private set; } = string.Empty;
    // Idade da pessoa, obrigatória
    public int Idade { get; private set; }

    // Construtor vazio necessário para o Entity Framework Core, pois ele precisa criar instâncias da classe para mapear os dados do banco de dados.
    protected Pessoa() { }


    // Construtor para criar uma nova pessoa, garantindo que os dados sejam validados e a entidade seja criada de forma consistente, seguindo os princípios de Rich Domain Model.
    public Pessoa(string nome, int idade)
    {
        ValidarDados(nome, idade);

        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
    }

    // Método para permitir edição controlada, garantindo que os dados sejam validados antes de serem atualizados.
    public void AtualizarDados(string nome, int idade)
    {
        ValidarDados(nome, idade);

        Nome = nome;
        Idade = idade;
    }


    // Método de validação de dados, garantindo o Rich Domain Model e evitando que a entidade seja criada ou atualizada com dados inválidos.
    private void ValidarDados(string nome, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (idade < 0)
            throw new ArgumentException("A idade não pode ser negativa.");
    }
}