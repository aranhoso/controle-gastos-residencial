import { usePessoas } from '../hooks/use-pessoas';
import { useTransacoes } from '../hooks/use-transacoes';

const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

type TotaisPessoa = {
  pessoaId: string;
  nome: string;
  receitas: number;
  despesas: number;
  saldo: number;
};

const TotaisPorPessoaPage = () => {
  const pessoasQuery = usePessoas();
  const transacoesQuery = useTransacoes();

  if (pessoasQuery.isLoading || transacoesQuery.isLoading) {
    return <section className="rounded-lg border p-4">Carregando totais por pessoa...</section>;
  }

  if (pessoasQuery.isError || transacoesQuery.isError) {
    return (
      <section className="rounded-lg border border-destructive/30 bg-destructive/5 p-4">
        Erro ao carregar totais por pessoa.
      </section>
    );
  }

  const pessoas = pessoasQuery.data ?? [];
  const transacoes = transacoesQuery.data ?? [];

  const totaisPorPessoa: TotaisPessoa[] = pessoas.map((pessoa) => {
    const transacoesPessoa = transacoes.filter((transacao) => transacao.pessoaId === pessoa.id);

    const receitas = transacoesPessoa
      .filter((transacao) => transacao.tipo === 1)
      .reduce((acc, transacao) => acc + transacao.valor, 0);

    const despesas = transacoesPessoa
      .filter((transacao) => transacao.tipo === 2)
      .reduce((acc, transacao) => acc + transacao.valor, 0);

    return {
      pessoaId: pessoa.id,
      nome: pessoa.nome,
      receitas,
      despesas,
      saldo: receitas - despesas,
    };
  });

  const totalGeral = totaisPorPessoa.reduce(
    (acc, totalPessoa) => {
      return {
        receitas: acc.receitas + totalPessoa.receitas,
        despesas: acc.despesas + totalPessoa.despesas,
        saldo: acc.saldo + totalPessoa.saldo,
      };
    },
    { receitas: 0, despesas: 0, saldo: 0 }
  );

  return (
    <section className="overflow-hidden rounded-lg border" aria-label="Página Totais por Pessoa">
      <table className="w-full text-sm">
        <thead className="bg-muted/50 text-left">
          <tr>
            <th className="px-4 py-3 font-medium">Pessoa</th>
            <th className="px-4 py-3 font-medium">Receitas</th>
            <th className="px-4 py-3 font-medium">Despesas</th>
            <th className="px-4 py-3 font-medium">Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totaisPorPessoa.map((item) => (
            <tr key={item.pessoaId} className="border-t">
              <td className="px-4 py-3">{item.nome}</td>
              <td className="px-4 py-3 text-emerald-600">{formatCurrency(item.receitas)}</td>
              <td className="px-4 py-3 text-rose-600">{formatCurrency(item.despesas)}</td>
              <td className="px-4 py-3">{formatCurrency(item.saldo)}</td>
            </tr>
          ))}
          <tr className="border-t bg-muted/30 font-semibold">
            <td className="px-4 py-3">Total geral</td>
            <td className="px-4 py-3 text-emerald-600">{formatCurrency(totalGeral.receitas)}</td>
            <td className="px-4 py-3 text-rose-600">{formatCurrency(totalGeral.despesas)}</td>
            <td className="px-4 py-3">{formatCurrency(totalGeral.saldo)}</td>
          </tr>
        </tbody>
      </table>
    </section>
  );
};

export default TotaisPorPessoaPage;
