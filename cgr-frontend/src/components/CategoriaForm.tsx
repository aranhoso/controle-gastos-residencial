import { useState, useEffect } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';
import { FinalidadeCategoria, type CategoriaRequestDTO, type CategoriaResponseDTO } from '../types/categorias';

interface CategoriaFormProps {
  initialData?: CategoriaResponseDTO | null;
  onSubmit: (data: CategoriaRequestDTO) => void;
  isLoading?: boolean;
}

export function CategoriaForm({ initialData, onSubmit, isLoading }: CategoriaFormProps) {
  const [descricao, setDescricao] = useState(initialData?.descricao ?? '');
  const [finalidade, setFinalidade] = useState<string>(
    initialData?.finalidade ? String(initialData.finalidade) : ''
  );

  useEffect(() => {
    if (initialData) {
      setDescricao(initialData.descricao);
      setFinalidade(String(initialData.finalidade));
    } else {
      setDescricao('');
      setFinalidade('');
    }
  }, [initialData]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!descricao || !finalidade) return;
    onSubmit({ descricao, finalidade: Number(finalidade) as FinalidadeCategoria });
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col gap-4 py-4">
      <div className="flex flex-col gap-2">
        <Label htmlFor="descricao">Descrição</Label>
        <Input
          id="descricao"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          placeholder="Ex: Supermercado"
          required
        />
      </div>
      <div className="flex flex-col gap-2">
        <Label htmlFor="finalidade">Finalidade</Label>
        <Select
          value={finalidade}
          onValueChange={(val) => setFinalidade(val || '')}
          required
        >
          <SelectTrigger>
            <SelectValue placeholder="Selecione..." />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value={String(FinalidadeCategoria.Receita)}>Receita</SelectItem>
            <SelectItem value={String(FinalidadeCategoria.Despesa)}>Despesa</SelectItem>
            <SelectItem value={String(FinalidadeCategoria.Ambas)}>Ambas</SelectItem>
          </SelectContent>
        </Select>
      </div>
      <Button type="submit" disabled={isLoading} className="mt-4">
        {isLoading ? 'Salvando...' : 'Salvar'}
      </Button>
    </form>
  );
}
