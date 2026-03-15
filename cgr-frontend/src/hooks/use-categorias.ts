import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { CategoriaService } from '../api/categoriaService';
import { invalidateAfterCategoriaMutation } from '../lib/query-invalidation';
import { queryKeys } from '../lib/query-keys';
import type { CategoriaRequestDTO } from '../types/categorias';

export function useCategorias() {
  return useQuery({
    queryKey: queryKeys.categorias.list(),
    queryFn: CategoriaService.obterTodas,
  });
}

export function useCategoriaById(id?: string) {
  return useQuery({
    queryKey: queryKeys.categorias.detail(id ?? ''),
    queryFn: async () => CategoriaService.obterPorId(id as string),
    enabled: Boolean(id),
  });
}

export function useCreateCategoria() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (dto: CategoriaRequestDTO) => CategoriaService.criar(dto),
    onSuccess: async (categoriaCriada) => {
      await invalidateAfterCategoriaMutation(queryClient, categoriaCriada.id);
    },
  });
}

type UpdateCategoriaPayload = {
  id: string;
  dto: CategoriaRequestDTO;
};

export function useUpdateCategoria() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, dto }: UpdateCategoriaPayload) => CategoriaService.atualizar(id, dto),
    onSuccess: async (categoriaAtualizada) => {
      await invalidateAfterCategoriaMutation(queryClient, categoriaAtualizada.id);
    },
  });
}

export function useDeleteCategoria() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => CategoriaService.deletar(id),
    onSuccess: async (_data, id) => {
      await invalidateAfterCategoriaMutation(queryClient, id);
    },
  });
}
