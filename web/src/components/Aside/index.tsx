import React, { ReactNode } from 'react';

type AsideProps = { children: ReactNode };

export default function Aside({ children }: AsideProps) {
  return (
    <aside className='class="card max-w-80 bg-white border border-gray-200 p-16 rounded'>
      {children}
    </aside>
  );
}
