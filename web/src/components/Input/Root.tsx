import React, { ReactNode } from 'react';

export default function Root({ children }: { children: ReactNode }) {
  return <div>{children}</div>;
}
