export const queryKeys = {
  pessoas: {
    all: ['pessoas'] as const,
    list: () => ['pessoas', 'list'] as const,
    detail: (id: string) => ['pessoas', 'detail', id] as const,
  },
  categorias: {
    all: ['categorias'] as const,
    list: () => ['categorias', 'list'] as const,
    detail: (id: string) => ['categorias', 'detail', id] as const,
  },
  transacoes: {
    all: ['transacoes'] as const,
    list: () => ['transacoes', 'list'] as const,
    detail: (id: string) => ['transacoes', 'detail', id] as const,
  },
  totais: {
    all: ['totais'] as const,
    porPessoa: () => ['totais', 'pessoas'] as const,
    porCategoria: () => ['totais', 'categorias'] as const,
  },
} as const;
