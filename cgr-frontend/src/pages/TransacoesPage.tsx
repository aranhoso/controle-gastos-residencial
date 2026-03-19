import { useState } from 'react';
import { useTransacoes, useCreateTransacao, useUpdateTransacao, useDeleteTransacao } from '../hooks/use-transacoes';
import { TransacaoForm } from '../components/TransacaoForm';
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetDescription } from '../components/ui/sheet';
import { Button } from '../components/ui/button';
import { Edit2Icon, PlusIcon, Trash2Icon } from 'lucide-react';
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from '../components/ui/alert-dialog';
import { toast } from 'sonner';
import type { TransacaoRequestDTO, TransacaoResponseDTO } from '../types/transacoes';

const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

const TransacoesPage = () => {
  const { data, isLoading, isError, error } = useTransacoes();
  const createMutation = useCreateTransacao();
  const updateMutation = useUpdateTransacao();
  const deleteMutation = useDeleteTransacao();

  const [isSheetOpen, setIsSheetOpen] = useState(false);
  const [editingTransacao, setEditingTransacao] = useState<TransacaoResponseDTO | null>(null);
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);
  const [transacaoToDelete, setTransacaoToDelete] = useState<string | null>(null);

  const handleOpenCreate = () => {
    setEditingTransacao(null);
    setIsSheetOpen(true);
  };

  const handleOpenEdit = (transacao: TransacaoResponseDTO) => {
    setEditingTransacao(transacao);
    setIsSheetOpen(true);
  };

  const handleSheetOpenChange = (open: boolean) => {
    setIsSheetOpen(open);
    if (!open) setEditingTransacao(null);
  };

  const handleSubmit = async (formData: TransacaoRequestDTO) => {
    try {
      if (editingTransacao) {
        await updateMutation.mutateAsync({ id: editingTransacao.id, dto: formData });
        toast.success('Transação atualizada com sucesso');
      } else {
        await createMutation.mutateAsync(formData);
        toast.success('Transação criada com sucesso');
      }
      setIsSheetOpen(false);
    } catch (err) {
      toast.error(err instanceof Error ? err.message : 'Erro ao salvar transação');
      throw err;
    }
  };

  const handleDeleteClick = (id: string) => {
    setTransacaoToDelete(id);
    setIsDeleteDialogOpen(true);
  };

  const confirmDelete = () => {
    if (!transacaoToDelete) return;
    deleteMutation.mutate(transacaoToDelete, {
      onSuccess: () => toast.success('Transação excluída com sucesso'),
      onError: (err) => toast.error(err instanceof Error ? err.message : 'Erro ao excluir transação'),
    });
    setIsDeleteDialogOpen(false);
    setTransacaoToDelete(null);
  };

  const isSaving = createMutation.isPending || updateMutation.isPending;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Transações</h2>
          <p className="text-muted-foreground">Gerencie as receitas e despesas.</p>
        </div>
        <Button onClick={handleOpenCreate}>
          <PlusIcon className="mr-2" />
          Nova Transação
        </Button>
      </div>

      <section className="overflow-hidden rounded-lg border" aria-label="Página Transações">
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="bg-muted/50 text-left">
              <tr>
                <th className="px-4 py-3 font-medium">Descrição</th>
                <th className="px-4 py-3 font-medium">Tipo</th>
                <th className="px-4 py-3 font-medium">Valor</th>
                <th className="px-4 py-3 font-medium">Pessoa</th>
                <th className="px-4 py-3 font-medium">Categoria</th>
                <th className="px-4 py-3 font-medium text-right">Ações</th>
              </tr>
            </thead>
            <tbody>
              {isLoading && (
                <tr>
                  <td colSpan={6} className="p-4 text-center">Carregando transações...</td>
                </tr>
              )}
              {isError && (
                <tr>
                  <td colSpan={6} className="p-4 text-center text-destructive">
                    Erro: {error instanceof Error ? error.message : 'inesperado'}
                  </td>
                </tr>
              )}
              {(!isLoading && !isError && (!data || data.length === 0)) && (
                <tr>
                  <td colSpan={6} className="p-4 text-center">Nenhuma transação cadastrada.</td>
                </tr>
              )}
              {data?.map((transacao) => (
                <tr key={transacao.id} className="border-t">
                  <td className="px-4 py-3">{transacao.descricao}</td>
                  <td className="px-4 py-3">
                    <span className={`inline-flex items-center rounded-full px-2 py-0.5 text-[0.7rem] font-semibold tracking-wide uppercase ${transacao.tipoDescricao === 'Receita' ? 'bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-400' : 'bg-red-100 text-red-800 dark:bg-red-900/30 dark:text-red-400'}`}>
                      {transacao.tipoDescricao}
                    </span>
                  </td>
                  <td className="px-4 py-3 whitespace-nowrap">{formatCurrency(transacao.valor)}</td>
                  <td className="px-4 py-3">{transacao.nomePessoa}</td>
                  <td className="px-4 py-3">{transacao.descricaoCategoria}</td>
                  <td className="px-4 py-3 text-right">
                    <div className="flex justify-end gap-2">
                      <Button variant="outline" size="icon-sm" onClick={() => handleOpenEdit(transacao)}>
                        <Edit2Icon />
                        <span className="sr-only">Editar</span>
                      </Button>
                      <Button variant="destructive" size="icon-sm" onClick={() => handleDeleteClick(transacao.id)}>
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
            <SheetTitle>{editingTransacao ? 'Editar Transação' : 'Nova Transação'}</SheetTitle>
            <SheetDescription>
              {editingTransacao ? 'Modifique os dados da transação.' : 'Preencha os dados para registrar a transação.'}
            </SheetDescription>
          </SheetHeader>
          <TransacaoForm
            key={editingTransacao ? editingTransacao.id : 'nova'}
            initialData={editingTransacao}
            onSubmit={handleSubmit}
            isLoading={isSaving}
          />
        </SheetContent>
      </Sheet>

      <AlertDialog open={isDeleteDialogOpen} onOpenChange={setIsDeleteDialogOpen}>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Excluir Transação</AlertDialogTitle>
            <AlertDialogDescription>
              Tem certeza que deseja excluir esta transação? Ela deixará de constar nos totais. Esta ação não pode ser desfeita.
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel onClick={() => setTransacaoToDelete(null)}>Cancelar</AlertDialogCancel>
            <AlertDialogAction onClick={confirmDelete} className="bg-destructive text-destructive-foreground hover:bg-destructive/90">
              Excluir
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </div>
  );
};

export default TransacoesPage;
