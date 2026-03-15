export const FinalidadeCategoria = {
  Receita: 1,
  Despesa: 2,
  Ambas: 3,
} as const;

export type FinalidadeCategoria =
  (typeof FinalidadeCategoria)[keyof typeof FinalidadeCategoria];

export interface CategoriaRequestDTO {
  descricao: string;
  finalidade: FinalidadeCategoria;
}

export interface CategoriaResponseDTO {
  id: string;
  descricao: string;
  finalidade: FinalidadeCategoria;
  finalidadeDescricao: string;
}