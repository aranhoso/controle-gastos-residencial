import { api } from './api';
import type { TransacaoRequestDTO, TransacaoResponseDTO } from '../types/transacoes';

export const TransacaoService = {
  obterTodas: async (): Promise<TransacaoResponseDTO[]> => {
    const response = await api.get<TransacaoResponseDTO[]>('/transacoes');
    return response.data;
  },

  obterPorId: async (id: string): Promise<TransacaoResponseDTO> => {
    const response = await api.get<TransacaoResponseDTO>(`/transacoes/${id}`);
    return response.data;
  },

  criar: async (dto: TransacaoRequestDTO): Promise<TransacaoResponseDTO> => {
    const response = await api.post<TransacaoResponseDTO>('/transacoes', dto);
    return response.data;
  },

  atualizar: async (id: string, dto: TransacaoRequestDTO): Promise<TransacaoResponseDTO> => {
    const response = await api.put<TransacaoResponseDTO>(`/transacoes/${id}`, dto);
    return response.data;
  },

  deletar: async (id: string): Promise<void> => {
    await api.delete(`/transacoes/${id}`);
  }
};
