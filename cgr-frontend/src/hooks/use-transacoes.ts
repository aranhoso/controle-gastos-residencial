import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { TransacaoService } from '../api/transacaoService';
import { invalidateAfterTransacaoMutation } from '../lib/query-invalidation';
import { queryKeys } from '../lib/query-keys';
import type { TransacaoRequestDTO } from '../types/transacoes';

export function useTransacoes() {
  return useQuery({
    queryKey: queryKeys.transacoes.list(),
    queryFn: TransacaoService.obterTodas,
  });
}

export function useTransacaoById(id?: string) {
  return useQuery({
    queryKey: queryKeys.transacoes.detail(id ?? ''),
    queryFn: async () => TransacaoService.obterPorId(id as string),
    enabled: Boolean(id),
  });
}

export function useCreateTransacao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (dto: TransacaoRequestDTO) => TransacaoService.criar(dto),
    onSuccess: async (transacaoCriada) => {
      await invalidateAfterTransacaoMutation(queryClient, transacaoCriada.id);
    },
  });
}

type UpdateTransacaoPayload = {
  id: string;
  dto: TransacaoRequestDTO;
};

export function useUpdateTransacao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, dto }: UpdateTransacaoPayload) =>
      TransacaoService.atualizar(id, dto),
    onSuccess: async (transacaoAtualizada) => {
      await invalidateAfterTransacaoMutation(queryClient, transacaoAtualizada.id);
    },
  });
}

export function useDeleteTransacao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => TransacaoService.deletar(id),
    onSuccess: async (_data, id) => {
      await invalidateAfterTransacaoMutation(queryClient, id);
    },
  });
}
