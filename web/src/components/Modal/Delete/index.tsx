'use client';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title?: string;
  description?: string;
}

export function ModalDelete({
  isOpen,
  onClose,
  onConfirm,
  title,
  description,
}: ModalProps) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-[90%] max-w-md">
        <h2 className="text-lg font-bold mb-4">{title || 'Confirmação'}</h2>
        <p className="text-gray-700 mb-6">
          {description || 'Tem certeza que deseja continuar?'}
        </p>
        <div className="flex justify-end gap-4">
          <button
            onClick={onClose}
            className="bg-gray-200 px-4 py-2 rounded hover:bg-gray-300 transition"
          >
            Cancelar
          </button>
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
}
