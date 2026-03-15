import { useState, type JSX } from 'react';
import './App.css';
import Navbar from './components/Navbar';
import CategoriasPage from './pages/CategoriasPage';
import DashboardPage from './pages/DashboardPage';
import PessoasPage from './pages/PessoasPage';
import TotaisPorCategoriaPage from './pages/TotaisPorCategoriaPage';
import TotaisPorPessoaPage from './pages/TotaisPorPessoaPage';
import TransacoesPage from './pages/TransacoesPage';

const navItems = [
  { key: 'dashboard', label: 'Dashboard' },
  { key: 'pessoas', label: 'Pessoas' },
  { key: 'categorias', label: 'Categorias' },
  { key: 'transacoes', label: 'Transações' },
  { key: 'totais-por-pessoa', label: 'Totais por Pessoa' },
  { key: 'totais-por-categoria', label: 'Totais por Categoria' },
] as const;

type PageKey = (typeof navItems)[number]['key'];

const pages: Record<PageKey, JSX.Element> = {
  dashboard: <DashboardPage />,
  pessoas: <PessoasPage />,
  categorias: <CategoriasPage />,
  transacoes: <TransacoesPage />,
  'totais-por-pessoa': <TotaisPorPessoaPage />,
  'totais-por-categoria': <TotaisPorCategoriaPage />,
};

function App() {
  const [activePage, setActivePage] = useState<PageKey>('dashboard');

  return (
    <div className="app-shell">
      <Navbar items={[...navItems]} activeKey={activePage} onSelect={(key) => setActivePage(key as PageKey)} />
      <main className="app-content">{pages[activePage]}</main>
    </div>
  );
}

export default App;
