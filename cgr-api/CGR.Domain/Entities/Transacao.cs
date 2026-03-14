using CGR.Domain.Enums;

namespace CGR.Domain.Entities;

public class Transacao
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; } = null!;
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;

    protected Transacao() { }

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

    public void AtualizarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa, Categoria categoria)
    {
        ValidarDados(descricao, valor, tipo, pessoa, categoria);
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        PessoaId = pessoa.Id;
        CategoriaId = categoria.Id;
    }

    private static void ValidarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa, Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.");
        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");
        if (!Enum.IsDefined(tipo))
            throw new ArgumentException("O tipo de transação é inválido.");
        if (pessoa == null)
            throw new ArgumentException("A pessoa é obrigatória.");
        if (categoria == null)
            throw new ArgumentException("A categoria é obrigatória.");

        #region Regras de negócio específicas

        if (pessoa.Idade < 18 && tipo == TipoTransacao.Receita)
            throw new ArgumentException("Menores de idade não podem registrar receitas.");

        if (tipo == TipoTransacao.Despesa && categoria.Finalidade == FinalidadeCategoria.Receita)
            throw new ArgumentException("Transação de despesa não pode usar uma categoria exclusiva de receita.");

        if (tipo == TipoTransacao.Receita && categoria.Finalidade == FinalidadeCategoria.Despesa)
            throw new ArgumentException("Transação de receita não pode usar uma categoria exclusiva de despesa.");

        #endregion
    }

}