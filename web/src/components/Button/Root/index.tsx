'use client';
import React, { ButtonHTMLAttributes } from 'react';

type RootProps = ButtonHTMLAttributes<HTMLButtonElement>;

export function Root({ children, className, ...props }: RootProps) {
  return (
    <button {...props} className={className}>
      {children}
    </button>
  );
}
