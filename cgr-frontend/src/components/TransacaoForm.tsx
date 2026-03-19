import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';

import { usePessoas } from '@/hooks/use-pessoas';
import { useCategorias } from '@/hooks/use-categorias';
import { TipoTransacao, type TransacaoRequestDTO, type TransacaoResponseDTO } from '@/types/transacoes';

import { useForm, Controller, type SubmitHandler, type Resolver } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';


const schema = z.object({
  descricao: z.string().min(1, 'Descrição obrigatória').max(400, 'Máximo de 400 caracteres'),
  valor: 
    z.preprocess((v) => typeof v === 'string' ? Number(v) : v, 
    z.number({ message: 'Digite um valor válido' }).positive('O valor deve ser maior que zero')),
  tipo: 
    z.preprocess((v) => typeof v === 'string' ? Number(v) : v, 
    z.union([z.literal(1), z.literal(2)], { message: 'Tipo inválido' })),
  pessoaId: z.uuid('Selecione uma pessoa'),
  categoriaId: z.uuid('Selecione uma categoria'),
});

type FormFields = z.infer<typeof schema>;

interface TransacaoFormProps {
  initialData?: TransacaoResponseDTO | null;
  onSubmit: (data: TransacaoRequestDTO) => Promise<void>;
  isLoading?: boolean;
}

export function TransacaoForm({ initialData, onSubmit, isLoading }: TransacaoFormProps) {

  const { data: pessoas } = usePessoas();
  const { data: categorias } = useCategorias();

  const {
    register,
    handleSubmit,
    control,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({
    resolver: zodResolver(schema) as Resolver<FormFields, unknown, FormFields>,
    defaultValues: initialData ? {
      descricao: initialData.descricao,
      valor: initialData.valor,
      tipo: initialData.tipo as 1 | 2,
      pessoaId: initialData.pessoaId,
      categoriaId: initialData.categoriaId,
    } : undefined
  });



  const onFormSubmit: SubmitHandler<FormFields> = async (data) => {
    try {
      await onSubmit({
        descricao: data.descricao,
        valor: data.valor,
        tipo: data.tipo,
        pessoaId: data.pessoaId,
        categoriaId: data.categoriaId,
      });
    } catch (error) {
      setError("root", {
        message: error instanceof Error ? error.message : 'Erro desconhecido ao salvar',
      });
    }
  }

  return (
    <form className="flex flex-col space-y-4 py-4" onSubmit={handleSubmit(onFormSubmit)}>
      
      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Descrição</Label>
        <Input
          {...register("descricao")}
          type="text"
          placeholder='Ex: Conta de Luz'
        />
        {errors.descricao && <span className='text-sm text-red-500'>{errors.descricao.message}</span>}
      </div>

      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Valor</Label>
        <Input
          {...register("valor")}
          type="number"
          step="0.01"
          placeholder='Ex: 150.50'
        />
        {errors.valor && <span className='text-sm text-red-500'>{errors.valor.message}</span>}
      </div>

      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Tipo</Label>
        <Controller
          name="tipo"
          control={control}
          render={({ field }) => (
            <Select 
              onValueChange={(val) => field.onChange(Number(val))} 
              // A MÁGICA ESTÁ AQUI: Trocamos defaultValue por value.
              // Se field.value existir, convertemos para string, senão mandamos vazio.
              value={field.value ? field.value.toString() : ""} 
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecione o tipo">
                  {field.value === TipoTransacao.Receita
                    ? "Receita"
                    : field.value === TipoTransacao.Despesa
                      ? "Despesa"
                      : undefined}
                </SelectValue>
              </SelectTrigger>
              <SelectContent>
                <SelectItem value={TipoTransacao.Receita.toString()}>Receita</SelectItem>
                <SelectItem value={TipoTransacao.Despesa.toString()}>Despesa</SelectItem>
              </SelectContent>
            </Select>
          )}
        />
        {errors.tipo && <span className='text-sm text-red-500'>{errors.tipo.message}</span>}
      </div>

      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Pessoa</Label>
        <Controller
          name="pessoaId"
          control={control}
          render={({ field }) => {
            const semPessoas = !pessoas || pessoas.length === 0;
            const pessoaSelecionada = pessoas?.find((p) => p.id === field.value);
            return (
              <Select
                onValueChange={field.onChange}
                value={field.value || ""}
                disabled={semPessoas}
              >
                <SelectTrigger>
                  <SelectValue placeholder={semPessoas ? "Nenhuma pessoa cadastrada" : "Selecione a pessoa..."}>
                    {pessoaSelecionada ? pessoaSelecionada.nome : undefined}
                  </SelectValue>
                </SelectTrigger>
                <SelectContent>
                  {!semPessoas ? (
                    pessoas.map((p) => (
                      <SelectItem key={p.id} value={p.id}>{p.nome}</SelectItem>
                    ))
                  ) : (
                    <div className="p-2 text-sm text-gray-500 text-center">
                      Você não possui nenhuma pessoa cadastrada.
                    </div>
                  )}
                </SelectContent>
              </Select>
            );
          }}
        />
        {errors.pessoaId && <span className='text-sm text-red-500'>{errors.pessoaId.message}</span>}
      </div>
      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Categoria</Label>
        <Controller
          name="categoriaId"
          control={control}
          render={({ field }) => {
            const semCategorias = !categorias || categorias.length === 0;
            const categoriaSelecionada = categorias?.find((c) => c.id === field.value);
            return (
              <Select
                onValueChange={field.onChange}
                value={field.value || ""}
                disabled={semCategorias}
              >
                <SelectTrigger>
                  <SelectValue placeholder={semCategorias ? "Nenhuma categoria cadastrada" : "Selecione a categoria..."}>
                    {categoriaSelecionada ? categoriaSelecionada.descricao : undefined}
                  </SelectValue>
                </SelectTrigger>
                <SelectContent>
                  {!semCategorias ? (
                    categorias.map((c) => (
                      <SelectItem key={c.id} value={c.id}>{c.descricao}</SelectItem>
                    ))
                  ) : (
                    <div className="p-2 text-sm text-gray-500 text-center">
                      Você não possui nenhuma categoria cadastrada.
                    </div>
                  )}
                </SelectContent>
              </Select>
            );
          }}
        />
        {errors.categoriaId && <span className='text-sm text-red-500'>{errors.categoriaId.message}</span>}
      </div>

      <Button disabled={isSubmitting || isLoading} type="submit" className="mt-2 ms-2 me-2">
        {isSubmitting || isLoading ? "Salvando..." : "Salvar Transação"}
      </Button>

      {errors.root && <div className='p-2 bg-red-100 text-red-700 rounded'>{errors.root.message}</div>}
    </form>
  );
}