import { Navigate, Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import CategoriasPage from './pages/CategoriasPage';
import DashboardPage from './pages/DashboardPage';
import PessoasPage from './pages/PessoasPage';
import TotaisPorCategoriaPage from './pages/TotaisPorCategoriaPage';
import TotaisPorPessoaPage from './pages/TotaisPorPessoaPage';
import TransacoesPage from './pages/TransacoesPage';

const navItems = [
  { to: '/dashboard', label: 'Dashboard' },
  { to: '/pessoas', label: 'Pessoas' },
  { to: '/categorias', label: 'Categorias' },
  { to: '/transacoes', label: 'Transações' },
  { to: '/totais-por-pessoa', label: 'Totais por Pessoa' },
  { to: '/totais-por-categoria', label: 'Totais por Categoria' },
] as const;

function App() {
  return (
    <div className="app-shell">
      <Navbar items={[...navItems]} />
      <main className="app-content">
        <Routes>
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/pessoas" element={<PessoasPage />} />
          <Route path="/categorias" element={<CategoriasPage />} />
          <Route path="/transacoes" element={<TransacoesPage />} />
          <Route path="/totais-por-pessoa" element={<TotaisPorPessoaPage />} />
          <Route path="/totais-por-categoria" element={<TotaisPorCategoriaPage />} />
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      </main>
    </div>
  );
}

export default App;
