import Image from 'next/image';

interface LogoProps {
  width: number;
  height: number;
}

export const Logo = ({ height, width }: LogoProps) => {
  return (
    <Image
      src="./images/logo.svg"
      width={width}
      height={height}
      alt="logo da aplicação"
    />
  );
};
