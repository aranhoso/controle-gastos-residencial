import { NavLink, useLocation } from 'react-router-dom';
import {
  Sidebar,
  SidebarContent,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarRail,
} from '@/components/ui/sidebar';
import {
  ChartColumnIncreasingIcon,
  CircleDollarSignIcon,
  FolderTreeIcon,
  HomeIcon,
  ReceiptTextIcon,
  UsersIcon,
} from 'lucide-react';

const navItems = [
  { title: 'Dashboard', to: '/dashboard', icon: HomeIcon },
  { title: 'Pessoas', to: '/pessoas', icon: UsersIcon },
  { title: 'Categorias', to: '/categorias', icon: FolderTreeIcon },
  { title: 'Transações', to: '/transacoes', icon: ReceiptTextIcon },
  {
    title: 'Totais por Pessoa',
    to: '/totais-por-pessoa',
    icon: CircleDollarSignIcon,
  },
  {
    title: 'Totais por Categoria',
    to: '/totais-por-categoria',
    icon: ChartColumnIncreasingIcon,
  },
] as const;

export function AppSidebar(props: React.ComponentProps<typeof Sidebar>) {
  const location = useLocation();

  return (
    <Sidebar collapsible="offcanvas" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton>
              <span className="text-base font-semibold">Controle de Gastos</span>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <SidebarMenu>
          {navItems.map((item) => (
            <SidebarMenuItem key={item.to}>
              <SidebarMenuButton
                isActive={location.pathname === item.to}
                render={<NavLink to={item.to} />}
              >
                <item.icon />
                <span>{item.title}</span>
              </SidebarMenuButton>
            </SidebarMenuItem>
          ))}
        </SidebarMenu>
      </SidebarContent>
      <SidebarRail />
    </Sidebar>
  );
}
