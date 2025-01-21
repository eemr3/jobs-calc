import React, { ReactNode } from 'react';

export default function Content({ children }: { children: ReactNode }) {
  return <div className="mt-2 relative">{children}</div>;
}
