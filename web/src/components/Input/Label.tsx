type LabelPros = {
  text: string;
  htmlFor: string;
  className: string;
};

export default function Label({ htmlFor, text, className }: LabelPros) {
  return (
    <label htmlFor={htmlFor} className={className}>
      {text}
    </label>
  );
}
