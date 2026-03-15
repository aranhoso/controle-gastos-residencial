import { useCategorias } from '../hooks/use-categorias';
import { usePessoas } from '../hooks/use-pessoas';
import { useTransacoes } from '../hooks/use-transacoes';

const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

const DashboardPage = () => {
  const pessoasQuery = usePessoas();
  const categoriasQuery = useCategorias();
  const transacoesQuery = useTransacoes();

  const isLoading = pessoasQuery.isLoading || categoriasQuery.isLoading || transacoesQuery.isLoading;
  const isError = pessoasQuery.isError || categoriasQuery.isError || transacoesQuery.isError;

  if (isLoading) {
    return <section className="rounded-lg border p-4">Carregando dados do dashboard...</section>;
  }

  if (isError) {
    return <section className="rounded-lg border border-destructive/30 bg-destructive/5 p-4">Erro ao carregar dados do dashboard.</section>;
  }

  const pessoas = pessoasQuery.data ?? [];
  const categorias = categoriasQuery.data ?? [];
  const transacoes = transacoesQuery.data ?? [];

  const totalReceitas = transacoes
    .filter((transacao) => transacao.tipo === 1)
    .reduce((acc, transacao) => acc + transacao.valor, 0);

  const totalDespesas = transacoes
    .filter((transacao) => transacao.tipo === 2)
    .reduce((acc, transacao) => acc + transacao.valor, 0);

  const saldo = totalReceitas - totalDespesas;

  return (
    <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-3" aria-label="Página Dashboard">
      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Pessoas cadastradas</h2>
        <p className="mt-2 text-3xl font-semibold">{pessoas.length}</p>
      </article>

      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Categorias cadastradas</h2>
        <p className="mt-2 text-3xl font-semibold">{categorias.length}</p>
      </article>

      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Transações cadastradas</h2>
        <p className="mt-2 text-3xl font-semibold">{transacoes.length}</p>
      </article>

      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Total de receitas</h2>
        <p className="mt-2 text-2xl font-semibold text-emerald-600">{formatCurrency(totalReceitas)}</p>
      </article>

      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Total de despesas</h2>
        <p className="mt-2 text-2xl font-semibold text-rose-600">{formatCurrency(totalDespesas)}</p>
      </article>

      <article className="rounded-lg border bg-card p-4">
        <h2 className="text-sm text-muted-foreground">Saldo</h2>
        <p className="mt-2 text-2xl font-semibold">{formatCurrency(saldo)}</p>
      </article>
    </section>
  );
};

export default DashboardPage;
