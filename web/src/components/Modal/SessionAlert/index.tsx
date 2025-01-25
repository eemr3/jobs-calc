// components/Modal.tsx
'use client';

import React from 'react';

interface ModalProps {
  isOpen: boolean;
  onConfirm: () => void;
}

export const ModalSessionAlert: React.FC<ModalProps> = ({ isOpen, onConfirm }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-[90%] max-w-md">
        <h2 className="text-lg font-bold mb-4">Sua sessão encerrou!</h2>
        <p className="text-gray-700 mb-6">Você será redirecionado para a tela de login</p>
        <div className="flex justify-end gap-4">
          <button
            onClick={onConfirm}
            className="bg-orange-400 text-white px-4 py-2 rounded hover:bg-orange-600 transition"
          >
            Confirmar
          </button>
        </div>
      </div>
    </div>
  );
};
