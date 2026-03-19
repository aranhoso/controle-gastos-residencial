import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import type { PessoaRequestDTO, PessoaResponseDTO } from '@/types/pessoas';
import { useEffect } from 'react';

import { useForm, type SubmitHandler, type Resolver } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';


const schema = z.object({
  nome: z.string().min(1, 'Nome obrigatório').max(200, 'Máximo de 200 caracteres'),
  idade:
    z.preprocess((v) => typeof v === 'string' ? Number(v) : v,
    z.number({ message: 'Digite uma idade válida' }).int('Idade deve ser um número inteiro').nonnegative('Idade deve ser positiva')),
});

type FormFields = z.infer<typeof schema>;

interface PessoaFormProps {
  initialData?: PessoaResponseDTO | null;
  onSubmit: (data: PessoaRequestDTO) => Promise<void>;
  isLoading?: boolean;
}

export function PessoaForm({ initialData, onSubmit, isLoading }: PessoaFormProps) {

  const {
    register,
    handleSubmit,
    setError,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({
    resolver: zodResolver(schema) as Resolver<FormFields, unknown, FormFields>,
    defaultValues: initialData ? {
      nome: initialData.nome,
      idade: initialData.idade,
    } : undefined
  });

  useEffect(() => {
    if (initialData) {
      reset({
        nome: initialData.nome,
        idade: initialData.idade,
      });
    } else {
      reset({ nome: '', idade: undefined });
    }
  }, [initialData, reset]);

  const onFormSubmit: SubmitHandler<FormFields> = async (data) => {
    try {
      await onSubmit({
        nome: data.nome,
        idade: data.idade,
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
        <Label>Nome</Label>
        <Input
          {...register("nome")}
          type="text"
          placeholder='Ex: João da Silva'
        />
        {errors.nome && <span className='text-sm text-red-500'>{errors.nome.message}</span>}
      </div>

      <div className="flex flex-col gap-1 ms-2 me-2">
        <Label>Idade</Label>
        <Input
          {...register("idade")}
          type="number"
          min="0"
          placeholder='Ex: 30'
        />
        {errors.idade && <span className='text-sm text-red-500'>{errors.idade.message}</span>}
      </div>

      <Button disabled={isSubmitting || isLoading} type="submit" className="mt-2 ms-2 me-2">
        {isSubmitting || isLoading ? "Salvando..." : "Salvar Pessoa"}
      </Button>

      {errors.root && <div className='p-2 bg-red-100 text-red-700 rounded'>{errors.root.message}</div>}
    </form>
  );
}
