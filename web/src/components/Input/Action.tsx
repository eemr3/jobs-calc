import React, { forwardRef, InputHTMLAttributes } from 'react';

type ActionProps = InputHTMLAttributes<HTMLInputElement> & {
  helperText?: string;
};

export const Action = forwardRef<HTMLInputElement, ActionProps>(
  ({ name = '', type = 'text', helperText = '', ...props }, ref) => {
    return (
      <div className="mt-2">
        <input ref={ref} {...props} type={type} name={name} />
        <p className="text-red-400 mt-2">{helperText}</p>
      </div>
    );
  },
);