import { EyeIcon, EyeSlashIcon } from '@heroicons/react/16/solid';
import React from 'react';

type EyeProps = {
  setShowPassword: (value: boolean) => void;
  showPassword: boolean;
};

export default function Eye({ showPassword, setShowPassword }: EyeProps) {
  return (
    <button
      className="absolute inset-y-0 end-0 flex items-center z-20 px-2.5 cursor-pointer text-gray-400 rounded-e-md focus:outline-none focus-visible:text-indigo-500 hover:text-indigo-500 transition-colors"
      type="button"
      onClick={() => setShowPassword(!showPassword)}
    >
      {showPassword ? (
        <EyeIcon className="h-6 font-extralight" />
      ) : (
        <EyeSlashIcon className="h-6 font-extralight" />
      )}
    </button>
  );
}
