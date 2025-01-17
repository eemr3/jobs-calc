import { ReactNode } from 'react';

interface ContentProps {
  children: ReactNode;
  className: string;
}

export function Content({ children, className }: ContentProps) {
  return <div className={className}>{children}</div>;
}
