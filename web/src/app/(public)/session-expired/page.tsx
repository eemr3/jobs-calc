import Link from 'next/link';
import React from 'react';

export default function SessionExpired() {
  return (
    <>
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <h1 className="font-bold text-[56px] text-center">
          <span className="text-[#FCFDFE]">Jobs</span>
          <span className="text-orange-400">Calc</span>
        </h1>
        <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-[#FEFCFD]">
          Sua sessão expirou!
          <p className="mt-10">
            <Link href="/sign-in">Ir para tela de login</Link>
          </p>
        </h2>
      </div>
    </>
  );
}
