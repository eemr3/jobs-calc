import { ReactNode } from 'react';

interface HeaderProps {
  children: ReactNode;
  className: string;
}

export function Root({ children, className }: HeaderProps) {
  return <header className={className}>{children}</header>;
}
