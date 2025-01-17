'use client';
import React from 'react';

interface RootProps {
  children: React.ReactNode;
  className: string;
}

export function Root({ children, className }: RootProps) {
  return <button className={className}>{children}</button>;
}
