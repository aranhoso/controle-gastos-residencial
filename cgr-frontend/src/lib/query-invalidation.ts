import type { QueryClient } from '@tanstack/react-query';
import { queryKeys } from './query-keys';

// Funções de invalidação de cache para cada entidade e cenários específicos de mutação

export async function invalidatePessoas(queryClient: QueryClient): Promise<void> {
  await queryClient.invalidateQueries({ queryKey: queryKeys.pessoas.all });
}

export async function invalidateCategorias(queryClient: QueryClient): Promise<void> {
  await queryClient.invalidateQueries({ queryKey: queryKeys.categorias.all });
}

export async function invalidateTransacoes(queryClient: QueryClient): Promise<void> {
  await queryClient.invalidateQueries({ queryKey: queryKeys.transacoes.all });
}

export async function invalidateTotais(queryClient: QueryClient): Promise<void> {
  await queryClient.invalidateQueries({ queryKey: queryKeys.totais.all });
}

// Invalidações específicas para detalhes de entidades

export async function invalidatePessoaDetail(
  queryClient: QueryClient,
  pessoaId: string
): Promise<void> {
  await queryClient.invalidateQueries({
    queryKey: queryKeys.pessoas.detail(pessoaId),
    exact: true,
  });
}

export async function invalidateCategoriaDetail(
  queryClient: QueryClient,
  categoriaId: string
): Promise<void> {
  await queryClient.invalidateQueries({
    queryKey: queryKeys.categorias.detail(categoriaId),
    exact: true,
  });
}

export async function invalidateTransacaoDetail(
  queryClient: QueryClient,
  transacaoId: string
): Promise<void> {
  await queryClient.invalidateQueries({
    queryKey: queryKeys.transacoes.detail(transacaoId),
    exact: true,
  });
}

// Funções de invalidação após mutações, considerando os impactos em cascata

export async function invalidateAfterPessoaMutation(
  queryClient: QueryClient,
  pessoaId?: string
): Promise<void> {
  await invalidatePessoas(queryClient);

  if (pessoaId) {
    await invalidatePessoaDetail(queryClient, pessoaId);
  }

  await invalidateTransacoes(queryClient);
  await invalidateTotais(queryClient);
}

export async function invalidateAfterCategoriaMutation(
  queryClient: QueryClient,
  categoriaId?: string
): Promise<void> {
  await invalidateCategorias(queryClient);

  if (categoriaId) {
    await invalidateCategoriaDetail(queryClient, categoriaId);
  }

  await invalidateTransacoes(queryClient);
  await invalidateTotais(queryClient);
}

export async function invalidateAfterTransacaoMutation(
  queryClient: QueryClient,
  transacaoId?: string
): Promise<void> {
  await invalidateTransacoes(queryClient);

  if (transacaoId) {
    await invalidateTransacaoDetail(queryClient, transacaoId);
  }

  await invalidateTotais(queryClient);
}
