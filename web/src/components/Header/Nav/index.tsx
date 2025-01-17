import { ReactNode } from 'react';

interface NavBarProps {
  children: ReactNode;
  className: string;
}

export function NavBar({ children, className }: NavBarProps) {
  return <nav className={className}>{children}</nav>;
}
