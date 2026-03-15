import { useState, useEffect } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Label } from './ui/label';
import type { PessoaRequestDTO, PessoaResponseDTO } from '../types/pessoas';

interface PessoaFormProps {
  initialData?: PessoaResponseDTO | null;
  onSubmit: (data: PessoaRequestDTO) => void;
  isLoading?: boolean;
}

export function PessoaForm({ initialData, onSubmit, isLoading }: PessoaFormProps) {
  const [nome, setNome] = useState(initialData?.nome ?? '');
  const [idade, setIdade] = useState<string>(initialData?.idade ? String(initialData.idade) : '');

  useEffect(() => {
    if (initialData) {
      setNome(initialData.nome);
      setIdade(String(initialData.idade));
    } else {
      setNome('');
      setIdade('');
    }
  }, [initialData]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!nome || !idade) return;
    onSubmit({ nome, idade: Number(idade) });
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col gap-4 py-4">
      <div className="flex flex-col gap-2">
        <Label htmlFor="nome">Nome</Label>
        <Input
          id="nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          placeholder="Ex: João da Silva"
          required
        />
      </div>
      <div className="flex flex-col gap-2">
        <Label htmlFor="idade">Idade</Label>
        <Input
          id="idade"
          type="number"
          min="0"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          placeholder="Ex: 30"
          required
        />
      </div>
      <Button type="submit" disabled={isLoading} className="mt-4">
        {isLoading ? 'Salvando...' : 'Salvar'}
      </Button>
    </form>
  );
}
