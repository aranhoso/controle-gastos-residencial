import { api } from './api';
import type { CategoriaRequestDTO, CategoriaResponseDTO } from '../types/categorias';

export const CategoriaService = {
  obterTodas: async (): Promise<CategoriaResponseDTO[]> => {
    const response = await api.get<CategoriaResponseDTO[]>('/categorias');
    return response.data;
  },

  obterPorId: async (id: string): Promise<CategoriaResponseDTO> => {
    const response = await api.get<CategoriaResponseDTO>(`/categorias/${id}`);
    return response.data;
  },

  criar: async (dto: CategoriaRequestDTO): Promise<CategoriaResponseDTO> => {
    const response = await api.post<CategoriaResponseDTO>('/categorias', dto);
    return response.data;
  },

  atualizar: async (id: string, dto: CategoriaRequestDTO): Promise<CategoriaResponseDTO> => {
    const response = await api.put<CategoriaResponseDTO>(`/categorias/${id}`, dto);
    return response.data;
  },

  deletar: async (id: string): Promise<void> => {
    await api.delete(`/categorias/${id}`);
  }
};