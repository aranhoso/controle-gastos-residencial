type NavItem = {
  key: string;
  label: string;
};

type NavbarProps = {
  items: NavItem[];
  activeKey: string;
  onSelect: (key: string) => void;
};

const Navbar = ({ items, activeKey, onSelect }: NavbarProps) => {
  return (
    <header className="navbar">
      <nav aria-label="Menu principal">
        <ul className="navbar-list">
          {items.map((item) => (
            <li key={item.key}>
              <button
                type="button"
                className={`navbar-link ${activeKey === item.key ? 'is-active' : ''}`}
                onClick={() => onSelect(item.key)}
              >
                {item.label}
              </button>
            </li>
          ))}
        </ul>
      </nav>
    </header>
  );
};

export default Navbar;
