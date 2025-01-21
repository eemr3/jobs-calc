interface LetterAvatarProps {
  fullName: string;
}

const getInitials = (fullName: string) => {
  const [firstName, lastName] = fullName.split(' ');
  if (firstName && lastName) {
    return `${firstName.charAt(0)}${lastName.charAt(0)}`.toUpperCase();
  }

  return firstName.charAt(0).toUpperCase();
};

export const LetterAvatar = ({ fullName }: LetterAvatarProps) => {
  return (
    <div
      className="flex justify-center items-center w-12 h-12 rounded-full 
      text-white font-bold text-xl uppercase"
    >
      {getInitials(fullName)}
    </div>
  );
};
