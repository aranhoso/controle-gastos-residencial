using System;
using System.Linq;
using CGR.Domain.Entities;
using CGR.Domain.Enums;

namespace CGR.Infrastructure.Context;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Pessoas.Any())
        {
            return;
        }

        var p1 = new Pessoa("Guilherme", 23);
        var p2 = new Pessoa("Marina", 32);
        var p3 = new Pessoa("Felipe", 25);
        var p4 = new Pessoa("Gustavo", 28);


        context.Pessoas.AddRange(p1, p2, p3, p4);

        var catReceitaFixa = new Categoria("Salário", FinalidadeCategoria.Receita);
        var catMoradia = new Categoria("Moradia (Aluguel/Luz)", FinalidadeCategoria.Despesa);
        var catSupermercado = new Categoria("Supermercado", FinalidadeCategoria.Despesa);
        var catSaude = new Categoria("Saúde", FinalidadeCategoria.Ambas);
        var catServico = new Categoria("Prestação de Serviço", FinalidadeCategoria.Receita);
        var catPet = new Categoria("Pet Shop", FinalidadeCategoria.Despesa);
        context.Categorias.AddRange(catReceitaFixa, catMoradia, catSupermercado, catSaude, catServico, catPet);

        var t1 = new Transacao("Salário de Janeiro", 6500.00m, TipoTransacao.Receita, p1, catReceitaFixa);
        var t2 = new Transacao("Conta de Luz", 250.50m, TipoTransacao.Despesa, p1, catMoradia);
        var t3 = new Transacao("Aluguel Mensal", 1500.00m, TipoTransacao.Despesa, p2, catMoradia);
        var t4 = new Transacao("Mercado da semana", 480.90m, TipoTransacao.Despesa, p1, catSupermercado);
        var t5 = new Transacao("Estorno de Remédio", 50.00m, TipoTransacao.Receita, p2, catSaude);
        var t6 = new Transacao("Manutenção de Ar Condicionado", 500.00m, TipoTransacao.Receita, p3, catServico);
        var t7 = new Transacao("Banho e tosa do Leo", 120.00m, TipoTransacao.Despesa, p3, catPet);
        var t8 = new Transacao("Serviço Autônomo", 50.00m, TipoTransacao.Receita, p4, catServico);
        
        context.Transacoes.AddRange(t1, t2, t3, t4, t5, t6, t7, t8);

        context.SaveChanges();
    }
}
