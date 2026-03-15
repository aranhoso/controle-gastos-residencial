export const TipoTransacao = {
  Receita: 1,
  Despesa: 2,
} as const;

export type TipoTransacao =
  (typeof TipoTransacao)[keyof typeof TipoTransacao];

export interface TransacaoRequestDTO {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: string;
  categoriaId: string;
}

export interface TransacaoResponseDTO {
  id: string;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  tipoDescricao: string;
  pessoaId: string;
  nomePessoa: string;
  categoriaId: string;
  descricaoCategoria: string;
}
