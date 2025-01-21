// components/SessionExpired.tsx
'use client';

import React, { useState } from 'react';
import { signOut } from 'next-auth/react';
import { Modal } from '../Modal';
import { deleteCookie } from 'cookies-next';

export const SessionExpired: React.FC = () => {
  const [isModalOpen, setModalOpen] = useState(true);

  const handleConfirm = () => {
    setModalOpen(false);
    deleteCookie('access_token');
    signOut({ callbackUrl: '/' }); // Redireciona para a página de login
  };

  return <Modal isOpen={isModalOpen} onConfirm={handleConfirm} />;
};
