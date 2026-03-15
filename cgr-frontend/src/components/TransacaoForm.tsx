import { useState, useEffect } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';
import { useCategorias } from '../hooks/use-categorias';
import { usePessoas } from '../hooks/use-pessoas';
import { TipoTransacao, type TransacaoRequestDTO, type TransacaoResponseDTO } from '../types/transacoes';

interface TransacaoFormProps {
  initialData?: TransacaoResponseDTO | null;
  onSubmit: (data: TransacaoRequestDTO) => void;
  isLoading?: boolean;
}

export function TransacaoForm({ initialData, onSubmit, isLoading }: TransacaoFormProps) {
  const { data: categorias } = useCategorias();
  const { data: pessoas } = usePessoas();

  const [descricao, setDescricao] = useState(initialData?.descricao ?? '');
  const [valor, setValor] = useState<string>(initialData?.valor ? String(initialData.valor) : '');
  const [tipo, setTipo] = useState<string>(initialData?.tipo ? String(initialData.tipo) : '');
  const [pessoaId, setPessoaId] = useState<string>(initialData?.pessoaId ?? '');
  const [categoriaId, setCategoriaId] = useState<string>(initialData?.categoriaId ?? '');

  useEffect(() => {
    if (initialData) {
      setDescricao(initialData.descricao);
      setValor(String(initialData.valor));
      setTipo(String(initialData.tipo));
      setPessoaId(initialData.pessoaId);
      setCategoriaId(initialData.categoriaId);
    } else {
      setDescricao('');
      setValor('');
      setTipo('');
      setPessoaId('');
      setCategoriaId('');
    }
  }, [initialData]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!descricao || !valor || !tipo || !pessoaId || !categoriaId) return;
    
    const valorNumerico = Number(valor);

    onSubmit({
      descricao,
      valor: valorNumerico,
      tipo: Number(tipo) as TipoTransacao,
      pessoaId,
      categoriaId,
    });
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col gap-4 py-4">
      <div className="flex flex-col gap-2">
        <Label htmlFor="descricao">Descrição</Label>
        <Input
          id="descricao"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          placeholder="Ex: Conta de Luz"
          required
        />
      </div>

      <div className="flex flex-col gap-2">
        <Label htmlFor="valor">Valor (R$)</Label>
        <Input
          id="valor"
          type="number"
          step="0.01"
          min="0"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          placeholder="Ex: 95,40"
          required
        />
      </div>

      <div className="flex flex-col gap-2">
        <Label htmlFor="tipo">Tipo da Transação</Label>
        <Select value={tipo} onValueChange={(val) => setTipo(val || '')} required>
          <SelectTrigger>
            <SelectValue placeholder="Selecione..." />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value={String(TipoTransacao.Receita)}>Receita</SelectItem>
            <SelectItem value={String(TipoTransacao.Despesa)}>Despesa</SelectItem>
          </SelectContent>
        </Select>
      </div>

      <div className="flex flex-col gap-2">
        <Label htmlFor="pessoa">Pessoa</Label>
        <Select value={pessoaId} onValueChange={(val) => setPessoaId(val || '')} required>
          <SelectTrigger>
            <SelectValue placeholder="Selecione a pessoa..." />
          </SelectTrigger>
          <SelectContent>
            {pessoas?.map((p) => (
              <SelectItem key={p.id} value={p.id}>{p.nome}</SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      <div className="flex flex-col gap-2">
        <Label htmlFor="categoria">Categoria</Label>
        <Select value={categoriaId} onValueChange={(val) => setCategoriaId(val || '')} required>
          <SelectTrigger>
            <SelectValue placeholder="Selecione a categoria..." />
          </SelectTrigger>
          <SelectContent>
            {categorias?.map((c) => (
              <SelectItem key={c.id} value={c.id}>{c.descricao}</SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      <Button type="submit" disabled={isLoading} className="mt-4">
        {isLoading ? 'Salvando...' : 'Salvar'}
      </Button>
    </form>
  );
}
