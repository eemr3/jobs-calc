import Image from 'next/image';
import React from 'react';
import { formatCurrency } from '../../../Utils/formatCurrency';

interface CardsProps {
  jobId?: string;
  name: string;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;

  // dailyHours: 0,
  // totalHours: 0,
  // remainingDays: 17,
  // valueJob: 2100.24,
  // status: true,
  // userId: 35
}
export function Cards({ name, remainingDays, status, valueJob }: CardsProps) {
  return (
    <div
      className={`border border-gray-200 
  grid grid-cols-[40%_18%_15%_20%_10%] items-center 
  py-6 px-8 
  rounded
  bg-white
  hover:bg-gradient-to-l 
  hover:from-transparent 
  hover:to-orange-50
  overflow-hidden
  relative
  before:absolute
  before:top-0
  before:left-0
  before:w-1
  before:h-0
  before:transition-all
  before:bg-orange-400
  hover:before:h-full
`}
    >
      <div className="text-[1.2rem] text-gray-700 font-bold">{name}</div>
      <div className="grid">
        <span className="font-bold text-gray-400 uppercase text-xs">Prazo</span>

        {status === true && remainingDays > 0 ? (
          remainingDays === 1 ? (
            <strong className="text-gray-700">{remainingDays} dia</strong>
          ) : (
            <strong className="text-gray-700">{remainingDays} dias</strong>
          )
        ) : (
          <strong className="text-red-500"> Esgotado </strong>
        )}
      </div>
      <div className="column grid">
        <span className="font-bold text-gray-400 uppercase text-xs">Valor</span>
        <strong className="text-gray-700">{formatCurrency(valueJob)}</strong>
      </div>
      <div
        className={`status badge column bg-gray-300 px-3 py-2 rounded-full w-fit justify-self-center text-sm ${
          status === false ? 'bg-red-100' : 'bg-green-100'
        }`}
      >
        {!status ? (
          <div className="text-red-600">Encerrado</div>
        ) : (
          <div className="text-green-600">Em andamento</div>
        )}
      </div>
      <div className="actions column flex gap-2">
        <p className="sr-only">Ações</p>
        <button
          className="border border-gray-200 p-2 rounded hover:bg-gray-100 transition-all"
          title="Editar Job"
        >
          <Image
            className="w-5"
            src="./images/edit-24.svg"
            alt="Editar Job"
            width={20}
            height={20}
          />
        </button>
        <button
          className="border border-gray-200 p-2 rounded hover:bg-red-100 transition-all"
          title="Excluir Job"
        >
          <Image
            className="w-5"
            src="./images/trash-24.svg"
            alt="Excluir Job"
            width={20}
            height={20}
          />
        </button>
      </div>
    </div>
  );
}
