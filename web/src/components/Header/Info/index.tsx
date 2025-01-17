interface InfoProps {
  value: number;
  className: string;
  text: string;
}

export function Info({ value, className, text }: InfoProps) {
  return (
    <div className={className}>
      <strong className="text-xl">{value}</strong>
      {text}
    </div>
  );
}
