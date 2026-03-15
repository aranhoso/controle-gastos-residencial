import { Navigate, Route, Routes } from 'react-router-dom';
import type { CSSProperties } from 'react';
import './App.css';
import { AppSidebar } from './components/app-sidebar';
import { SiteHeader } from './components/site-header';
import { SidebarInset, SidebarProvider } from './components/ui/sidebar';
import CategoriasPage from './pages/CategoriasPage';
import DashboardPage from './pages/DashboardPage';
import PessoasPage from './pages/PessoasPage';
import TotaisPorCategoriaPage from './pages/TotaisPorCategoriaPage';
import TotaisPorPessoaPage from './pages/TotaisPorPessoaPage';
import TransacoesPage from './pages/TransacoesPage';

function AppLayout() {
  return (
    <SidebarProvider style={{ '--header-height': '3.5rem' } as CSSProperties}>
      <AppSidebar />
      <SidebarInset>
        <SiteHeader />
        <section className="flex flex-1 flex-col gap-4 p-4 md:p-6">
          <Routes>
            <Route path="/dashboard" element={<DashboardPage />} />
            <Route path="/pessoas" element={<PessoasPage />} />
            <Route path="/categorias" element={<CategoriasPage />} />
            <Route path="/transacoes" element={<TransacoesPage />} />
            <Route path="/totais-por-pessoa" element={<TotaisPorPessoaPage />} />
            <Route path="/totais-por-categoria" element={<TotaisPorCategoriaPage />} />
            <Route path="*" element={<Navigate to="/dashboard" replace />} />
          </Routes>
        </section>
      </SidebarInset>
    </SidebarProvider>
  );
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/dashboard" replace />} />
      <Route path="/*" element={<AppLayout />} />
    </Routes>
  );
}

export default App;
