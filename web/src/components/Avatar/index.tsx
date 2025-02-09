'use client';
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react';
import { deleteCookie } from 'cookies-next';
import { signOut } from 'next-auth/react';
import Image from 'next/image';
import Link from 'next/link';
import { UserProps } from '../../libs/types/typesAndInterfaces';
import { LetterAvatar } from '../LetterAvatar';

const baseUrl =
  process.env.NEXT_PUBLIC_CONTAINER === 'false'
    ? `http://localhost:5043`
    : `http://backend:8080`;

export function Avatar({ user }: UserProps) {
  const handleSignOut = () => {
    deleteCookie('access_token');

    signOut({ callbackUrl: '/sign-in' });
  };

  return (
    <div className="flex flex-col items-center hover:text-orange-400 transition">
      {/* Profile dropdown */}
      <Menu as="div" className="relative ml-3">
        <div>
          <MenuButton className="relative flex rounded-full bg-gray-800 text-sm focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
            <span className="absolute -inset-1.5" />
            <span className="sr-only">Abrir menu</span>
            {user.avatarUrl ? (
              <Image
                alt=""
                src={`${baseUrl}${user.avatarUrl}`}
                className="size-14 rounded-full"
                width={56}
                height={56}
              />
            ) : (
              <LetterAvatar fullName={user.fullName} />
            )}
          </MenuButton>
        </div>
        <MenuItems
          transition
          className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black/5 transition focus:outline-none data-[closed]:scale-95 data-[closed]:transform data-[closed]:opacity-0 data-[enter]:duration-100 data-[leave]:duration-75 data-[enter]:ease-out data-[leave]:ease-in"
        >
          <MenuItem>
            <Link
              href="/profile"
              className="block px-4 py-2 text-sm text-center text-gray-700 data-[focus]:bg-gray-100 data-[focus]:outline-none"
            >
              Ver perfil
            </Link>
          </MenuItem>

          <MenuItem>
            <button
              onClick={handleSignOut}
              className="block px-4 py-2 text-sm  text-gray-700 data-[focus]:bg-gray-100
                w-48 data-[focus]:outline-none"
            >
              Sign out
            </button>
          </MenuItem>
        </MenuItems>
      </Menu>
      <div className="grid mt-2">
        <strong>{user.fullName}</strong>
        <span className="text-xs">Ver perfil</span>
      </div>
    </div>
  );
}
