import { useMemo } from 'react';
import { useLocation } from 'react-router-dom';
import { Separator } from '@/components/ui/separator';
import { SidebarTrigger } from '@/components/ui/sidebar';

const pageTitleByPath: Record<string, string> = {
  '/dashboard': 'Dashboard',
  '/pessoas': 'Pessoas',
  '/categorias': 'Categorias',
  '/transacoes': 'Transações',
  '/totais-por-pessoa': 'Totais por Pessoa',
  '/totais-por-categoria': 'Totais por Categoria',
};

export function SiteHeader() {
  const location = useLocation();

  const pageTitle = useMemo(() => {
    return pageTitleByPath[location.pathname] ?? 'Dashboard';
  }, [location.pathname]);

  return (
    <header className="flex h-(--header-height) shrink-0 items-center gap-2 border-b transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-(--header-height)">
      <div className="flex w-full items-center gap-1 px-4 lg:gap-2 lg:px-6">
        <SidebarTrigger className="-ml-1" />
        <Separator
          orientation="vertical"
          className="mx-2 h-4 data-vertical:self-auto"
        />
        <h1 className="text-base font-medium">{pageTitle}</h1>
      </div>
    </header>
  );
}
