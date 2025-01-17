import { ReactNode } from 'react';

interface SummaryProps {
  children: ReactNode;
  className: string;
}

export function Summary({ children, className }: SummaryProps) {
  return <section className={className}>{children}</section>;
}
