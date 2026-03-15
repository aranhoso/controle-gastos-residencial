import { api } from './api';
import type { PessoaRequestDTO, PessoaResponseDTO } from '../types/pessoas';

export const PessoaService = {
  obterTodas: async (): Promise<PessoaResponseDTO[]> => {
    const response = await api.get<PessoaResponseDTO[]>('/pessoas');
    return response.data;
  },

  obterPorId: async (id: string): Promise<PessoaResponseDTO> => {
    const response = await api.get<PessoaResponseDTO>(`/pessoas/${id}`);
    return response.data;
  },

  criar: async (dto: PessoaRequestDTO): Promise<PessoaResponseDTO> => {
    const response = await api.post<PessoaResponseDTO>('/pessoas', dto);
    return response.data;
  },

  atualizar: async (id: string, dto: PessoaRequestDTO): Promise<PessoaResponseDTO> => {
    const response = await api.put<PessoaResponseDTO>(`/pessoas/${id}`, dto);
    return response.data;
  },

  deletar: async (id: string): Promise<void> => {
    await api.delete(`/pessoas/${id}`);
  }
};
