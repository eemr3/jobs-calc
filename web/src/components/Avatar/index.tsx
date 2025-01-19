import Image from 'next/image';

type UserProps = {
  user: User;
};

type User = {
  userId: number;
  fullName: string;
  email: string;
  avatarUrl: string;
};
export function Avatar(user: UserProps) {
  return (
    <button className="flex items-center gap-4 hover:text-orange-400 transition">
      <div className="grid text-end">
        <strong>{user.user.fullName}</strong>
        <span className="text-xs">Ver perfil</span>
      </div>
      {user.user.avatarUrl ? (
        <Image
          className="w-14 border-2 border-orange-400 rounded-full"
          src={`http://localhost:5043${user.user.avatarUrl}`}
          alt=""
          width={56}
          height={56}
        />
      ) : (
        <div className="w-[56px] h-[56px] bg-gray-300"></div>
      )}
    </button>
  );
}
