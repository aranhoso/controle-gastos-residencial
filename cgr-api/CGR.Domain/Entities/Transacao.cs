using CGR.Domain.Enums;

namespace CGR.Domain.Entities;

/// <summary>
/// Representa uma transação financeira (receita ou despesa) no sistema.
/// É a entidade central que conecta uma <see cref="Pessoa"/> a uma <see cref="Categoria"/>,
/// garantindo o cumprimento das regras de negócio de idade e compatibilidade de finalidade.
/// </summary>
public class Transacao
{
    /// <summary>
    /// Obtém o identificador único da transação, gerado automaticamente.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Obtém a descrição da transação. Limite máximo de 400 caracteres.
    /// </summary>
    public string Descricao { get; private set; } = string.Empty;

    /// <summary>
    /// Obtém o valor monetário da transação. Deve ser sempre estritamente positivo (maior que zero).
    /// </summary>
    public decimal Valor { get; private set; }

    /// <summary>
    /// Obtém o tipo da transação, indicando se é uma entrada (Receita) ou saída (Despesa) de dinheiro.
    /// </summary>
    public TipoTransacao Tipo { get; private set; }

    /// <summary>
    /// Obtém o identificador (Chave Estrangeira) da pessoa que realizou a transação.
    /// </summary>
    public Guid PessoaId { get; private set; }

    /// <summary>
    /// Obtém a propriedade de navegação da pessoa associada à transação.
    /// </summary>
    public Pessoa Pessoa { get; private set; } = null!;

    /// <summary>
    /// Obtém o identificador (Chave Estrangeira) da categoria associada à transação.
    /// </summary>
    public Guid CategoriaId { get; private set; }

    /// <summary>
    /// Obtém a propriedade de navegação da categoria que classifica esta transação.
    /// </summary>
    public Categoria Categoria { get; private set; } = null!;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Transacao"/>. 
    /// Construtor protegido exigido pelo Entity Framework Core para mapeamento de dados.
    /// </summary>
    protected Transacao() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Transacao"/>, validando todos os dados
    /// e regras de negócio antes de permitir a criação do objeto na memória.
    /// </summary>
    /// <param name="descricao">O texto descritivo da transação.</param>
    /// <param name="valor">O valor financeiro (deve ser positivo).</param>
    /// <param name="tipo">A natureza da transação (Receita ou Despesa).</param>
    /// <param name="pessoa">A entidade da pessoa dona da transação (usada para validar regras de idade).</param>
    /// <param name="categoria">A entidade da categoria da transação (usada para validar compatibilidade).</param>
    /// <exception cref="ArgumentException">Lançada se os dados básicos forem inválidos ou se as regras de negócio forem violadas.</exception>
    public Transacao(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa, Categoria categoria)
    {
        ValidarDados(descricao, valor, tipo, pessoa, categoria);
        
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        PessoaId = pessoa.Id;
        CategoriaId = categoria.Id;
    }

    /// <summary>
    /// Atualiza os dados da transação, reavaliando todas as regras de negócio de acordo com
    /// as novas informações fornecidas.
    /// </summary>
    /// <param name="descricao">A nova descrição da transação.</param>
    /// <param name="valor">O novo valor da transação.</param>
    /// <param name="tipo">O novo tipo da transação.</param>
    /// <param name="pessoa">A entidade pessoa atualizada.</param>
    /// <param name="categoria">A entidade categoria atualizada.</param>
    /// <exception cref="ArgumentException">Lançada se os novos dados violarem as regras do domínio.</exception>
    public void AtualizarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa, Categoria categoria)
    {
        ValidarDados(descricao, valor, tipo, pessoa, categoria);
        
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        PessoaId = pessoa.Id;
        CategoriaId = categoria.Id;
    }

    /// <summary>
    /// Concentra todas as validações estruturais e regras de negócio cruzadas do sistema.
    /// </summary>
    /// <param name="descricao">A descrição a ser validada.</param>
    /// <param name="valor">O valor a ser validado.</param>
    /// <param name="tipo">O tipo a ser validado.</param>
    /// <param name="pessoa">A pessoa a ser analisada para a regra de menor de idade.</param>
    /// <param name="categoria">A categoria a ser analisada para a regra de compatibilidade.</param>
    /// <exception cref="ArgumentException">Lançada detalhando qual regra específica foi quebrada.</exception>
    private static void ValidarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa, Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.");

        if (descricao.Length > 400)
            throw new ArgumentException("A descrição não pode exceder 400 caracteres.");

        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");
            
        if (!Enum.IsDefined(tipo))
            throw new ArgumentException("O tipo de transação é inválido.");
            
        if (pessoa == null)
            throw new ArgumentException("A pessoa é obrigatória.");
            
        if (categoria == null)
            throw new ArgumentException("A categoria é obrigatória.");

        #region Regras de negócio específicas

        // Menores de idade só podem registrar despesas, não receitas
        if (pessoa.Idade < 18 && tipo == TipoTransacao.Receita)
            throw new ArgumentException("Menores de idade não podem registrar receitas.");

        // Transação de despesa não pode usar uma categoria exclusiva de receita.
        if (tipo == TipoTransacao.Despesa && categoria.Finalidade == FinalidadeCategoria.Receita)
            throw new ArgumentException("Transação de despesa não pode usar uma categoria exclusiva de receita.");

        // Transação de receita não pode usar uma categoria exclusiva de despesa.
        if (tipo == TipoTransacao.Receita && categoria.Finalidade == FinalidadeCategoria.Despesa)
            throw new ArgumentException("Transação de receita não pode usar uma categoria exclusiva de despesa.");

        #endregion
    }

}