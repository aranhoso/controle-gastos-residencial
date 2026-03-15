import { NavLink } from 'react-router-dom';

type NavItem = {
  to: string;
  label: string;
  end?: boolean;
};

type NavbarProps = {
  items: NavItem[];
};

const Navbar = ({ items }: NavbarProps) => {
  return (
    <header className="navbar">
      <nav aria-label="Menu principal">
        <ul className="navbar-list">
          {items.map((item) => (
            <li key={item.to}>
              <NavLink
                to={item.to}
                end={item.end}
                className={({ isActive }) => `navbar-link ${isActive ? 'is-active' : ''}`}
              >
                {item.label}
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
    </header>
  );
};

export default Navbar;
