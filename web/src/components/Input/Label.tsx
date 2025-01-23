type LabelProps = {
  text: string;
  htmlFor: string;
  className: string;
  isHtml?: boolean; // Prop opcional para indicar se o texto contém HTML
};

export default function Label({ htmlFor, text, className, isHtml }: LabelProps) {
  return (
    <label
      htmlFor={htmlFor}
      className={className}
      {...(isHtml ? { dangerouslySetInnerHTML: { __html: text } } : { children: text })}
    />
  );
}
