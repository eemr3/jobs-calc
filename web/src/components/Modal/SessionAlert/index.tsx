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
    <div className="fixed inset-0 bg-gray-700 bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white rounded-lg shadow-lg w-[28rem] p-6">
        <div className="text-center">
          <h2 className="text-xl font-semibold mb-2">Sua sessão encerrou!</h2>
          <h3 className="text-lg font-semibold mb-4">
            Você será redirecionado para a tela de login
          </h3>
          <button
            className="bg-orange-400 text-white py-2 px-4 rounded hover:bg-orange-600 focus:outline-none"
            onClick={onConfirm}
          >
            Entendi
          </button>
        </div>
      </div>
    </div>
  );
};
