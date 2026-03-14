namespace CGR.Domain.Enums;

/// <summary>
/// Especifica os tipos de transação financeira suportados pelo sistema.
/// </summary>
/// <remarks>
/// Use este enum para indicar se uma transação representa uma receita (entrada de recursos) ou uma despesa (saída de recursos).
/// </remarks>
public enum TipoTransacao
{
    Receita = 1,
    Despesa = 2
}

/// <summary>
/// Especifica os possíveis propósitos de uma categoria financeira, indicando se ela é usada para receitas, despesas ou
/// ambas.
/// </summary>
/// <remarks>
/// Use este enum para classificar categorias em sistemas financeiros ou contábeis, facilitando a distinção entre entradas, saídas e categorias que abrangem ambos os tipos de transação.
/// </remarks>
public enum FinalidadeCategoria
{
    Receita = 1,
    Despesa = 2,
    Ambas = 3
}