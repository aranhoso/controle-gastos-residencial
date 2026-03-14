namespace CGR.Domain.Entities;

/// <summary>
/// Representa uma pessoa, com um identificador único, nome e idade. Esta entidade pode ser utilizada para associar transações a pessoas específicas, permitindo um controle mais detalhado dos gastos residenciais. A classe segue os princípios de Rich Domain Model, garantindo que os dados sejam validados e a entidade seja criada de forma consistente.
/// </summary>
/// <param name="nome">O nome da pessoa.</param>
/// <param name="idade">A idade da pessoa.</param>
/// <exception cref="ArgumentException">Lançada se o nome for vazio/maior que 200 caracteres, ou se a idade for negativa.</exception>
public class Pessoa
{
    /// <summary>
    /// Obtém o identificador único da pessoa, gerado automaticamente. Este identificador é utilizado para diferenciar cada pessoa no sistema e pode ser associado a transações para um controle mais detalhado dos gastos residenciais.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Obtém o nome da pessoa. Este campo é obrigatório e não pode ser nulo ou vazio.
    /// </summary>
    public string Nome { get; private set; } = string.Empty;

    /// <summary>
    /// Obtém a idade da pessoa. Este campo é obrigatório e não pode ser negativo.
    /// </summary>
    public int Idade { get; private set; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Pessoa"/>. 
    /// Construtor sem parâmetros exigido pelo Entity Framework Core.
    /// </summary>
    protected Pessoa() { }


    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Pessoa"/> com os dados fornecidos, garantindo que os dados sejam validados e a entidade seja criada de forma consistente, seguindo os princípios de Rich Domain Model.
    /// </summary>
    /// <param name="nome">O nome da pessoa.</param>
    /// <param name="idade">A idade da pessoa.</param>
    public Pessoa(string nome, int idade)
    {
        ValidarDados(nome, idade);

        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
    }

    /// <summary>
    /// Atualiza os dados da pessoa, garantindo que os dados sejam validados antes de serem aplicados.
    /// </summary>
    /// <param name="nome">O novo nome da pessoa.</param>
    /// <param name="idade">A nova idade da pessoa.</param>
    public void AtualizarDados(string nome, int idade)
    {
        ValidarDados(nome, idade);

        Nome = nome;
        Idade = idade;
    }

    /// <summary>
    /// Valida os dados fornecidos para a criação ou atualização da pessoa, garantindo que o nome não seja nulo ou vazio e que a idade não seja negativa. Caso os dados sejam inválidos, uma exceção do tipo <see cref="ArgumentException"/> é lançada, indicando o motivo da falha na validação.
    /// </summary> 
    /// <param name="nome">O nome da pessoa a ser validado.</param>
    /// <param name="idade">A idade da pessoa a ser validada.</param>
    private static void ValidarDados(string nome, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (idade < 0)
            throw new ArgumentException("A idade não pode ser negativa.");

        if (nome.Length > 200)
            throw new ArgumentException("O nome não pode exceder 200 caracteres.");
    }
}