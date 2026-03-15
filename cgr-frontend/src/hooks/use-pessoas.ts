import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { PessoaService } from '../api/pessoaService';
import { invalidateAfterPessoaMutation } from '../lib/query-invalidation';
import { queryKeys } from '../lib/query-keys';
import type { PessoaRequestDTO } from '../types/pessoas';

export function usePessoas() {
  return useQuery({
    queryKey: queryKeys.pessoas.list(),
    queryFn: PessoaService.obterTodas,
  });
}

export function usePessoaById(id?: string) {
  return useQuery({
    queryKey: queryKeys.pessoas.detail(id ?? ''),
    queryFn: async () => PessoaService.obterPorId(id as string),
    enabled: Boolean(id),
  });
}

export function useCreatePessoa() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (dto: PessoaRequestDTO) => PessoaService.criar(dto),
    onSuccess: async (pessoaCriada) => {
      await invalidateAfterPessoaMutation(queryClient, pessoaCriada.id);
    },
  });
}

type UpdatePessoaPayload = {
  id: string;
  dto: PessoaRequestDTO;
};

export function useUpdatePessoa() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, dto }: UpdatePessoaPayload) => PessoaService.atualizar(id, dto),
    onSuccess: async (pessoaAtualizada) => {
      await invalidateAfterPessoaMutation(queryClient, pessoaAtualizada.id);
    },
  });
}

export function useDeletePessoa() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => PessoaService.deletar(id),
    onSuccess: async (_data, id) => {
      await invalidateAfterPessoaMutation(queryClient, id);
    },
  });
}
