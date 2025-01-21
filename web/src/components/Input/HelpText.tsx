import React from 'react';

export default function HelpText({
  helpText,
  className,
}: {
  helpText?: string;
  className?: string;
}) {
  return <p className={className}>{helpText}</p>;
}
