'use client';
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react';
import { deleteCookie, getCookie } from 'cookies-next';
import { signOut } from 'next-auth/react';
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
export function Avatar({ user }: UserProps) {
  const handleSignOut = () => {
    deleteCookie('access_token');

    signOut({ callbackUrl: '/' });
  };

  return (
    <>
      {/* Profile dropdown */}
      <Menu as="div" className="relative ml-3">
        <div>
          <MenuButton className="relative flex rounded-full bg-gray-800 text-sm focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
            <span className="absolute -inset-1.5" />
            <span className="sr-only">Abrir menu</span>
            <Image
              alt=""
              src={`http://localhost:5043${user.avatarUrl}`}
              className="size-14 rounded-full"
              width={56}
              height={56}
            />
          </MenuButton>
        </div>
        <MenuItems
          transition
          className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black/5 transition focus:outline-none data-[closed]:scale-95 data-[closed]:transform data-[closed]:opacity-0 data-[enter]:duration-100 data-[leave]:duration-75 data-[enter]:ease-out data-[leave]:ease-in"
        >
          <MenuItem>
            <a
              href="#"
              className="block px-4 py-2 text-sm text-gray-700 data-[focus]:bg-gray-100 data-[focus]:outline-none"
            >
              Ver perfil
            </a>
          </MenuItem>

          <MenuItem>
            <button
              onClick={handleSignOut}
              className="block px-4 py-2 text-sm text-gray-700 data-[focus]:bg-gray-100 data-[focus]:outline-none"
            >
              Sign out
            </button>
          </MenuItem>
        </MenuItems>
      </Menu>
    </>
  );
}

// import Image from 'next/image';

// type UserProps = {
//   user: User;
// };

// type User = {
//   userId: number;
//   fullName: string;
//   email: string;
//   avatarUrl: string;
// };
// export function Avatar(user: UserProps) {
//   return (
//     <button className="flex items-center gap-4 hover:text-orange-400 transition">
//       <div className="grid text-end">
//         <strong>{user.user.fullName}</strong>
//         <span className="text-xs">Ver perfil</span>
//       </div>
//       {user.user.avatarUrl ? (
//         <Image
//           className="w-14 border-2 border-orange-400 rounded-full"
//           src={`http://localhost:5043${user.user.avatarUrl}`}
//           alt=""
//           width={56}
//           height={56}
//         />
//       ) : (
//         <div className="w-[56px] h-[56px] bg-gray-300"></div>
//       )}
//     </button>
//   );
// }
