import { useState } from 'react';
import { usePessoas, useCreatePessoa, useUpdatePessoa, useDeletePessoa } from '../hooks/use-pessoas';
import { PessoaForm } from '../components/PessoaForm';
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetDescription } from '../components/ui/sheet';
import { Button } from '../components/ui/button';
import { Edit2Icon, PlusIcon, Trash2Icon } from 'lucide-react';
import { toast } from 'sonner';
import type { PessoaRequestDTO, PessoaResponseDTO } from '../types/pessoas';

const PessoasPage = () => {
  const { data, isLoading, isError, error } = usePessoas();
  const createMutation = useCreatePessoa();
  const updateMutation = useUpdatePessoa();
  const deleteMutation = useDeletePessoa();

  const [isSheetOpen, setIsSheetOpen] = useState(false);
  const [editingPessoa, setEditingPessoa] = useState<PessoaResponseDTO | null>(null);

  const handleOpenCreate = () => {
    setEditingPessoa(null);
    setIsSheetOpen(true);
  };

  const handleOpenEdit = (pessoa: PessoaResponseDTO) => {
    setEditingPessoa(pessoa);
    setIsSheetOpen(true);
  };

  const handleSheetOpenChange = (open: boolean) => {
    setIsSheetOpen(open);
    if (!open) setEditingPessoa(null);
  };

  const handleSubmit = async (formData: PessoaRequestDTO) => {
    try {
      if (editingPessoa) {
        await updateMutation.mutateAsync({ id: editingPessoa.id, dto: formData });
        toast.success('Pessoa atualizada com sucesso');
      } else {
        await createMutation.mutateAsync(formData);
        toast.success('Pessoa criada com sucesso');
      }
      setIsSheetOpen(false);
    } catch (err) {
      toast.error(err instanceof Error ? err.message : 'Erro ao salvar pessoa');
      throw err;
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Tem certeza que deseja excluir esta pessoa?')) {
      deleteMutation.mutate(id, {
        onSuccess: () => toast.success('Pessoa excluída com sucesso'),
        onError: (err) => toast.error(err instanceof Error ? err.message : 'Erro ao excluir pessoa'),
      });
    }
  };

  const isSaving = createMutation.isPending || updateMutation.isPending;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Pessoas</h2>
          <p className="text-muted-foreground">Gerencie as pessoas que participam das despesas residenciais.</p>
        </div>
        <Button onClick={handleOpenCreate}>
          <PlusIcon className="mr-2" />
          Nova Pessoa
        </Button>
      </div>

      <section className="overflow-hidden rounded-lg border" aria-label="Página Pessoas">
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="bg-muted/50 text-left">
              <tr>
                <th className="px-4 py-3 font-medium">Nome</th>
                <th className="px-4 py-3 font-medium">Idade</th>
                <th className="px-4 py-3 font-medium">Identificador</th>
                <th className="px-4 py-3 font-medium text-right">Ações</th>
              </tr>
            </thead>
            <tbody>
              {isLoading && (
                <tr>
                  <td colSpan={4} className="p-4 text-center">Carregando pessoas...</td>
                </tr>
              )}
              {isError && (
                <tr>
                  <td colSpan={4} className="p-4 text-center text-destructive">
                    Erro: {error instanceof Error ? error.message : 'inesperado'}
                  </td>
                </tr>
              )}
              {(!isLoading && !isError && (!data || data.length === 0)) && (
                <tr>
                  <td colSpan={4} className="p-4 text-center">Nenhuma pessoa cadastrada.</td>
                </tr>
              )}
              {data?.map((pessoa) => (
                <tr key={pessoa.id} className="border-t">
                  <td className="px-4 py-3">{pessoa.nome}</td>
                  <td className="px-4 py-3">{pessoa.idade}</td>
                  <td className="px-4 py-3 text-muted-foreground">{pessoa.id}</td>
                  <td className="px-4 py-3 text-right">
                    <div className="flex justify-end gap-2">
                      <Button variant="outline" size="icon-sm" onClick={() => handleOpenEdit(pessoa)}>
                        <Edit2Icon />
                        <span className="sr-only">Editar</span>
                      </Button>
                      <Button variant="destructive" size="icon-sm" onClick={() => handleDelete(pessoa.id)}>
                        <Trash2Icon />
                        <span className="sr-only">Excluir</span>
                      </Button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>

      <Sheet open={isSheetOpen} onOpenChange={handleSheetOpenChange}>
        <SheetContent>
          <SheetHeader>
            <SheetTitle>{editingPessoa ? 'Editar Pessoa' : 'Nova Pessoa'}</SheetTitle>
            <SheetDescription>
              {editingPessoa ? 'Modifique os dados da pessoa.' : 'Preencha os dados para criar a pessoa.'}
            </SheetDescription>
          </SheetHeader>
          <PessoaForm
            initialData={editingPessoa}
            onSubmit={handleSubmit}
            isLoading={isSaving}
          />
        </SheetContent>
      </Sheet>
    </div>
  );
};

export default PessoasPage;
