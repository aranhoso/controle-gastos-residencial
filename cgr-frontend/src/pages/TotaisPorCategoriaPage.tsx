import { useCategorias } from '../hooks/use-categorias';
import { useTransacoes } from '../hooks/use-transacoes';

const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

type TotaisCategoria = {
  categoriaId: string;
  descricao: string;
  receitas: number;
  despesas: number;
  saldo: number;
};

const TotaisPorCategoriaPage = () => {
  const categoriasQuery = useCategorias();
  const transacoesQuery = useTransacoes();

  if (categoriasQuery.isLoading || transacoesQuery.isLoading) {
    return <section className="rounded-lg border p-4">Carregando totais por categoria...</section>;
  }

  if (categoriasQuery.isError || transacoesQuery.isError) {
    return (
      <section className="rounded-lg border border-destructive/30 bg-destructive/5 p-4">
        Erro ao carregar totais por categoria.
      </section>
    );
  }

  const categorias = categoriasQuery.data ?? [];
  const transacoes = transacoesQuery.data ?? [];

  const totaisPorCategoria: TotaisCategoria[] = categorias.map((categoria) => {
    const transacoesCategoria = transacoes.filter(
      (transacao) => transacao.categoriaId === categoria.id
    );

    const receitas = transacoesCategoria
      .filter((transacao) => transacao.tipo === 1)
      .reduce((acc, transacao) => acc + transacao.valor, 0);

    const despesas = transacoesCategoria
      .filter((transacao) => transacao.tipo === 2)
      .reduce((acc, transacao) => acc + transacao.valor, 0);

    return {
      categoriaId: categoria.id,
      descricao: categoria.descricao,
      receitas,
      despesas,
      saldo: receitas - despesas,
    };
  });

  const totalGeral = totaisPorCategoria.reduce(
    (acc, totalCategoria) => {
      return {
        receitas: acc.receitas + totalCategoria.receitas,
        despesas: acc.despesas + totalCategoria.despesas,
        saldo: acc.saldo + totalCategoria.saldo,
      };
    },
    { receitas: 0, despesas: 0, saldo: 0 }
  );

  return (
    <section className="overflow-hidden rounded-lg border" aria-label="Página Totais por Categoria">
      <table className="w-full text-sm">
        <thead className="bg-muted/50 text-left">
          <tr>
            <th className="px-4 py-3 font-medium">Categoria</th>
            <th className="px-4 py-3 font-medium">Receitas</th>
            <th className="px-4 py-3 font-medium">Despesas</th>
            <th className="px-4 py-3 font-medium">Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totaisPorCategoria.map((item) => (
            <tr key={item.categoriaId} className="border-t">
              <td className="px-4 py-3">{item.descricao}</td>
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

export default TotaisPorCategoriaPage;
