'use client';
import Image from 'next/image';
import Link from 'next/link';
import { ChangeEvent, useEffect, useState } from 'react';
import { formatCurrency } from '../../libs/formatCurrency';
import Aside from '../Aside';
import { Button } from '../Button';
import { Header } from '../Header';
import { useMutation } from '@apollo/client';
import { useRouter } from 'next/navigation';
import toast from 'react-hot-toast';
import { JobData } from '../../libs/types/typesAndInterfaces';
import { CREATE_JOB, UPDATE_JOB } from '../../service/graphql/mutations/jobMutation';

export function ProjectComponent({ job, planning, isEditMode }: JobData) {
  const [createJob] = useMutation(CREATE_JOB);
  const [updateJob] = useMutation(UPDATE_JOB);

  const route = useRouter();
  const [projectValue, setProjectValue] = useState(0);
  const [formData, setFormData] = useState({
    name: job?.name,
    dailyHours: job?.dailyHours,
    totalHours: job?.totalHours,
  });

  const calculateProjectValue = () => {
    if (formData.totalHours && planning.valueHour) {
      return formData.totalHours * planning.valueHour;
    }

    return 0;
  };
  useEffect(() => {
    if (isEditMode) {
      setProjectValue(calculateProjectValue());
    }
  }, [isEditMode]);

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleBlur = () => {
    setProjectValue(calculateProjectValue());
  };

  const handleSubmit = async () => {
    try {
      if (isEditMode && job?.jobId) {
        await updateJob({
          variables: {
            input: {
              name: formData.name,
              dailyHours: Number(formData.dailyHours),
              totalHours: Number(formData.totalHours),
            },
            jobId: job.jobId,
          },
        });

        route.push('/'); //redirect dashboard
      } else {
        await createJob({
          variables: {
            input: {
              name: formData.name,
              dailyHours: Number(formData.dailyHours),
              totalHours: Number(formData.totalHours),
            },
          },
        });
        route.push('/'); //redirect dashboard
      }
    } catch (error) {
      toast.error('Houve um problema ao tentar salvar o projeto.');
    }
  };

  return (
    <div className="bg-gray-100 min-h-screen">
      <Header.Root className="py-8 bg-gray-700 p-12">
        <Header.NavBar className="">
          <div className="animate-up text-gray-300 font-semibold flex items-center">
            <Link href="/">
              <Image src="/images/back.svg" alt="" width={24} height={24} />
            </Link>
            <h1 className="mx-auto">Projetos</h1>
          </div>
        </Header.NavBar>
      </Header.Root>
      <div className="animate-up delay-2 flex p-12 max-w-4xl mx-auto gap-16">
        <div className="w-1/2">
          <main>
            <h2 className="text-3xl font-medium text-gray-600 border-b pb-4 mb-4">
              Dados do projeto
            </h2>

            <div className="space-y-4">
              <div className="grid gap-3">
                <label htmlFor="name" className="text-gray-500 font-medium text-sm">
                  Nome do projeto
                </label>
                <input
                  className="px-4 py-2 border rounded-sm text-sm"
                  type="text"
                  id="name"
                  name="name"
                  value={formData?.name}
                  onChange={handleChange}
                />
              </div>

              <div className="flex gap-4">
                <div className="grid gap-3">
                  <label
                    htmlFor="daily-hours"
                    className="text-gray-500 font-medium text-sm"
                  >
                    Quantas horas por dia vai dedicar ao job?
                  </label>
                  <input
                    className="px-4 py-2 border rounded-sm text-sm"
                    type="number"
                    step="1"
                    id="daily-hours"
                    name="dailyHours"
                    value={formData?.dailyHours}
                    onChange={handleChange}
                  />
                </div>

                <div className="grid gap-3">
                  <label
                    htmlFor="total-hours"
                    className="text-gray-500 font-medium text-sm"
                  >
                    Estimativa de horas para esse Job?
                  </label>
                  <input
                    className="px-4 py-2 border rounded-sm text-sm"
                    type="number"
                    id="total-hours"
                    name="totalHours"
                    value={formData?.totalHours}
                    onChange={handleChange}
                    onBlur={handleBlur}
                  />
                </div>
              </div>
            </div>
          </main>
        </div>
        <div className="flex-grow-0 text-center">
          <Aside>
            <img
              className="mx-auto"
              src="/images/money-color.svg"
              alt="Imagem de Dinheiro"
            />
            <p className="mt-8 text-gray-600">
              O valor do projeto ficou em <strong>{formatCurrency(projectValue)}</strong>
            </p>
            <div className="mt-10">
              <Button.Root
                onClick={handleSubmit}
                className="bg-green-500 px-20 py-2 rounded text-[#FCFDFE] hover:bg-green-600 hover:text-gray-200"
              >
                Salvar
              </Button.Root>
            </div>
          </Aside>
        </div>
      </div>
    </div>
  );
}
