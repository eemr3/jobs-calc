// components/SessionExpired.tsx
'use client';

import React, { useState } from 'react';
import { signOut } from 'next-auth/react';
import { ModalSessionAlert } from '../Modal/SessionAlert';
import { deleteCookie } from 'cookies-next';

export const SessionExpired: React.FC = () => {
  const [isModalOpen, setModalOpen] = useState(true);

  const handleConfirm = async () => {
    setModalOpen(false);
    deleteCookie('access_token');
    await signOut({ callbackUrl: '/sign-in' }); // Redireciona para a página de login
  };

  return <ModalSessionAlert isOpen={isModalOpen} onConfirm={handleConfirm} />;
};
