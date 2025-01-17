import Image from 'next/image';

export function Avatar() {
  return (
    <button className="flex items-center gap-4 hover:text-orange-400 transition">
      <div className="grid text-end">
        <strong>Emrson Moreira</strong>
        <span className="text-xs">Ver perfil</span>
      </div>
      <Image
        className="w-14 border-2 border-orange-400 rounded-full"
        src="https://avatars.githubusercontent.com/u/42968718?v=4"
        alt=""
        width={56}
        height={56}
      />
    </button>
  );
}
