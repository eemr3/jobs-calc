import React, { forwardRef, InputHTMLAttributes } from 'react';

type ActionProps = InputHTMLAttributes<HTMLInputElement>;

export const Action = forwardRef<HTMLInputElement, ActionProps>(
  ({ id, name = '', type = 'text', ...props }, ref) => {
    return <input ref={ref} id={id} {...props} type={type} name={name} />;
  },
);
