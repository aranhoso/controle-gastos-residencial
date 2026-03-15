import { useState } from 'react';
import { useCategorias, useCreateCategoria, useUpdateCategoria, useDeleteCategoria } from '../hooks/use-categorias';
import { CategoriaForm } from '../components/CategoriaForm';
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetDescription } from '../components/ui/sheet';
import { Button } from '../components/ui/button';
import { Edit2Icon, PlusIcon, Trash2Icon } from 'lucide-react';
import { toast } from 'sonner';
import type { CategoriaRequestDTO, CategoriaResponseDTO } from '../types/categorias';

const CategoriasPage = () => {
  const { data, isLoading, isError, error } = useCategorias();
  const createMutation = useCreateCategoria();
  const updateMutation = useUpdateCategoria();
  const deleteMutation = useDeleteCategoria();

  const [isSheetOpen, setIsSheetOpen] = useState(false);
  const [editingCategoria, setEditingCategoria] = useState<CategoriaResponseDTO | null>(null);

  const handleOpenCreate = () => {
    setEditingCategoria(null);
    setIsSheetOpen(true);
  };

  const handleOpenEdit = (categoria: CategoriaResponseDTO) => {
    setEditingCategoria(categoria);
    setIsSheetOpen(true);
  };

  const handleSheetOpenChange = (open: boolean) => {
    setIsSheetOpen(open);
    if (!open) setEditingCategoria(null);
  };

  const handleSubmit = (formData: CategoriaRequestDTO) => {
    if (editingCategoria) {
      updateMutation.mutate(
        { id: editingCategoria.id, dto: formData },
        {
          onSuccess: () => {
            toast.success('Categoria atualizada com sucesso');
            setIsSheetOpen(false);
          },
          onError: (err) => {
            toast.error(err instanceof Error ? err.message : 'Erro ao atualizar categoria');
          },
        }
      );
    } else {
      createMutation.mutate(formData, {
        onSuccess: () => {
          toast.success('Categoria criada com sucesso');
          setIsSheetOpen(false);
        },
        onError: (err) => {
          toast.error(err instanceof Error ? err.message : 'Erro ao criar categoria');
        },
      });
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Tem certeza que deseja excluir esta categoria?')) {
      deleteMutation.mutate(id, {
        onSuccess: () => toast.success('Categoria excluída com sucesso'),
        onError: (err) => toast.error(err instanceof Error ? err.message : 'Erro ao excluir categoria'),
      });
    }
  };

  const isSaving = createMutation.isPending || updateMutation.isPending;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Categorias</h2>
          <p className="text-muted-foreground">Gerencie as categorias de receitas e despesas.</p>
        </div>
        <Button onClick={handleOpenCreate}>
          <PlusIcon className="mr-2" />
          Nova Categoria
        </Button>
      </div>

      <section className="overflow-hidden rounded-lg border" aria-label="Página Categorias">
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="bg-muted/50 text-left">
              <tr>
                <th className="px-4 py-3 font-medium">Descrição</th>
                <th className="px-4 py-3 font-medium">Finalidade</th>
                <th className="px-4 py-3 font-medium">Identificador</th>
                <th className="px-4 py-3 font-medium text-right">Ações</th>
              </tr>
            </thead>
            <tbody>
              {isLoading && (
                <tr>
                  <td colSpan={4} className="p-4 text-center">Carregando categorias...</td>
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
                  <td colSpan={4} className="p-4 text-center">Nenhuma categoria cadastrada.</td>
                </tr>
              )}
              {data?.map((categoria) => (
                <tr key={categoria.id} className="border-t">
                  <td className="px-4 py-3">{categoria.descricao}</td>
                  <td className="px-4 py-3">{categoria.finalidadeDescricao}</td>
                  <td className="px-4 py-3 text-muted-foreground">{categoria.id}</td>
                  <td className="px-4 py-3 text-right">
                    <div className="flex justify-end gap-2">
                      <Button variant="outline" size="icon-sm" onClick={() => handleOpenEdit(categoria)}>
                        <Edit2Icon />
                        <span className="sr-only">Editar</span>
                      </Button>
                      <Button variant="destructive" size="icon-sm" onClick={() => handleDelete(categoria.id)}>
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
            <SheetTitle>{editingCategoria ? 'Editar Categoria' : 'Nova Categoria'}</SheetTitle>
            <SheetDescription>
              {editingCategoria ? 'Modifique os dados da categoria.' : 'Preencha os dados para criar a categoria.'}
            </SheetDescription>
          </SheetHeader>
          <CategoriaForm
            initialData={editingCategoria}
            onSubmit={handleSubmit}
            isLoading={isSaving}
          />
        </SheetContent>
      </Sheet>
    </div>
  );
};

export default CategoriasPage;
