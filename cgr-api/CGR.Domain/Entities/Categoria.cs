using CGR.Domain.Enums;

namespace CGR.Domain.Entities;

/// <summary>
/// Representa uma categoria utilizada para classificar as transações (ex: Moradia, Salário, Alimentação).
/// </summary>
public class Categoria
{
    /// <summary>
    /// Obtém o identificador único da categoria, gerado automaticamente.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Obtém a descrição da categoria. Limite máximo de 400 caracteres.
    /// </summary>
    public string Descricao { get; private set; } = string.Empty;

    /// <summary>
    /// Obtém a finalidade da categoria, que restringe se ela pode ser usada para Receitas, Despesas ou Ambas.
    /// </summary>
    public FinalidadeCategoria Finalidade { get; private set; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Categoria"/>. 
    /// Construtor sem parâmetros exigido pelo Entity Framework Core.
    /// </summary>
    protected Categoria() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Categoria"/> com os dados fornecidos, garantindo que os dados sejam validados e a entidade seja criada de forma consistente, seguindo os princípios de Rich Domain Model.
    /// </summary>
    /// <param name="descricao">O nome descritivo da categoria (ex: Moradia, Lazer).</param>
    /// <param name="finalidade">A regra de uso da categoria, definindo se aceita apenas receitas, despesas ou ambas.</param>
    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        ValidarDados(descricao, finalidade);
        Id = Guid.NewGuid();
        Descricao = descricao;
        Finalidade = finalidade;
    }

    /// <summary>
    /// Atualiza a descrição e a finalidade da categoria após validar os dados fornecidos.
    /// </summary>
    /// <remarks>
    /// Utilize este método para garantir que as alterações nos dados da categoria sejam validadas
    /// antes de serem aplicadas. Caso os dados sejam inválidos, uma exceção pode ser lançada pelo método de
    /// validação.
    /// </remarks>
    /// <param name="descricao">A nova descrição que substituirá a atual.</param>
    /// <param name="finalidade">A nova regra de finalidade da categoria.</param>
    public void AtualizarDados(string descricao, FinalidadeCategoria finalidade)
    {
        ValidarDados(descricao, finalidade);
        Descricao = descricao;
        Finalidade = finalidade;
    }

    /// <summary>
    /// Valida os dados fornecidos para a criação ou atualização da categoria, garantindo que a descrição não seja nula ou vazia e que a finalidade seja um valor válido do enum FinalidadeCategoria.
    /// </summary>
    /// <param name="descricao">A descrição a ser validada.</param>
    /// <param name="finalidade">A finalidade a ser validada.</param>
    /// <exception cref="ArgumentException">Lançada quando a descrição está em branco ou a finalidade não existe no enumerador.</exception>
    private static void ValidarDados(string descricao, FinalidadeCategoria finalidade)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.");
        if (descricao.Length > 400)
            throw new ArgumentException("A descrição não pode exceder 400 caracteres.");
        if (!Enum.IsDefined(finalidade))
            throw new ArgumentException("A finalidade de categoria inválida.");
    }
}
