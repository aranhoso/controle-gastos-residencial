import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';

import { FinalidadeCategoria, type CategoriaRequestDTO, type CategoriaResponseDTO } from '@/types/categorias';

import { useForm, Controller, type SubmitHandler, type Resolver } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';


const schema = z.object({
  descricao: z.string().min(1, 'Descrição obrigatória').max(200, 'Máximo de 200 caracteres'),
  finalidade:
    z.preprocess((v) => typeof v === 'string' ? Number(v) : v,
    z.union([z.literal(1), z.literal(2), z.literal(3)], { message: 'Finalidade inválida' })),
});

type FormFields = z.infer<typeof schema>;

interface CategoriaFormProps {
  initialData?: CategoriaResponseDTO | null;
  onSubmit: (data: CategoriaRequestDTO) => Promise<void>;
  isLoading?: boolean;
}

export function CategoriaForm({ initialData, onSubmit, isLoading }: CategoriaFormProps) {

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
      finalidade: initialData.finalidade as 1 | 2 | 3,
    } : undefined
  });



  const onFormSubmit: SubmitHandler<FormFields> = async (data) => {
    try {
      await onSubmit({
        descricao: data.descricao,
        finalidade: data.finalidade,
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
          placeholder='Ex: Supermercado'
        />
        {errors.descricao && <span className='text-sm text-red-500'>{errors.descricao.message}</span>}
      </div>

      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Finalidade</Label>
        <Controller
          name="finalidade"
          control={control}
          render={({ field }) => (
            <Select 
              onValueChange={(val) => field.onChange(Number(val))}
              value={field.value ? field.value.toString() : ""}
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecione a finalidade">
                  {field.value === FinalidadeCategoria.Receita
                    ? "Receita"
                    : field.value === FinalidadeCategoria.Despesa
                      ? "Despesa"
                      : field.value === FinalidadeCategoria.Ambas
                        ? "Ambas"
                        : undefined}
                </SelectValue>
              </SelectTrigger>
              <SelectContent>
                <SelectItem value={FinalidadeCategoria.Receita.toString()}>Receita</SelectItem>
                <SelectItem value={FinalidadeCategoria.Despesa.toString()}>Despesa</SelectItem>
                <SelectItem value={FinalidadeCategoria.Ambas.toString()}>Ambas</SelectItem>
              </SelectContent>
            </Select>
          )}
        />
        {errors.finalidade && <span className='text-sm text-red-500'>{errors.finalidade.message}</span>}
      </div>

      <Button disabled={isSubmitting || isLoading} type="submit" className="mt-2 ms-2 me-2">
        {isSubmitting || isLoading ? "Salvando..." : "Salvar Categoria"}
      </Button>

      {errors.root && <div className='p-2 bg-red-100 text-red-700 rounded'>{errors.root.message}</div>}
    </form>
  );
}
