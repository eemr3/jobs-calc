'use client';
import { useMutation } from '@apollo/client';
import { PencilSquareIcon } from '@heroicons/react/16/solid';
import Image from 'next/image';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { ChangeEvent, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { formatCurrency } from '../../libs/formatCurrency';
import { Planning, ProfileProps, User } from '../../libs/types/typesAndInterfaces';
import {
  CREATE_PLANNING,
  UPDATE_PLANNING,
} from '../../service/graphql/mutations/planningMutation';
import { UPDATE_USER } from '../../service/graphql/mutations/userMutation';
import { uploadAvatar } from '../../service/rest/rest-requests';
import Aside from '../Aside';
import { Button } from '../Button';
import { Header } from '../Header';
import { Input } from '../Input';

const initialAvatar = '/images/profile-defaul.svg';

const baseUrl =
  process.env.NEXT_PUBLIC_DEVELOPMENT === 'true'
    ? `http://localhost:5043`
    : `http://backend:8080`;

export default function ProfileComponent({ user, planning }: ProfileProps) {
  const [createPlanning] = useMutation(CREATE_PLANNING);
  const [updatePlanning] = useMutation(UPDATE_PLANNING);
  const [updateUser] = useMutation(UPDATE_USER);

  const router = useRouter();
  const [profileImage, setProfileImage] = useState(initialAvatar);
  const [userData, setUserData] = useState<Partial<User>>({ fullName: user.fullName });
  const [planningData, setPlanningData] = useState<Planning>(
    getInitialPlanningData(planning),
  );

  useEffect(() => {
    if (user.avatarUrl) {
      setProfileImage(`${baseUrl}${user.avatarUrl}`);
    }
  }, [user.avatarUrl]);

  const handleChangeInput = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setPlanningData((prev) => ({ ...prev, [name]: value }));
  };

  const handleImageUpload = async (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    if (!validateFile(file)) return;

    try {
      const data = new FormData();
      data.set('file', file);
      const response = await uploadAvatar(data);

      if (response) {
        setProfileImage(`${baseUrl}${response.avatar_url}`);
        toast.success('Avatar atualizado com sucesso!');
      }
    } catch (error: any) {
      if (error.response?.status === 401) {
        toast.error('Sessão expirada. Redirecionando para o login...');
        return router.push('/sign-in');
      } else {
        toast.error('Erro ao enviar a imagem. Tente novamente.');
      }
    }
  };

  function validateFile(file: File): boolean {
    const maxSize = 2 * 1024 * 1024;
    const allowedExtensions = ['jpg', 'jpeg', 'png'];
    const fileExtension = file.name.split('.').pop()?.toLowerCase();

    if (file.size > maxSize) {
      toast.error('O arquivo excede o tamanho máximo de 2 MB');
      return false;
    }

    if (!allowedExtensions.includes(fileExtension || '')) {
      toast.error('Apenas arquivos JPG, JPEG e PNG são permitidos.');
      return false;
    }

    return true;
  }

  const handleSubmit = async () => {
    const payload = {
      daysPerWeek: Number(planningData.daysPerWeek),
      hoursPerDay: Number(planningData.hoursPerDay),
      monthlyBudget: Number(planningData.monthlyBudget),
      vacationPerYear: Number(planningData.vacationPerYear),
    };

    try {
      const isNewPlanning =
        planningData.planningId === '00000000-0000-0000-0000-000000000000';

      const { data } = isNewPlanning
        ? await createPlanning({
            variables: {
              input: payload,
            },
          })
        : await updatePlanning({
            variables: {
              input: { ...payload, planningId: planningData.planningId },
            },
          });

      if (!isNewPlanning) {
        await updateUser({
          variables: {
            input: {
              fullName: userData.fullName,
            },
          },
        });
      }

      if (data) {
        if (data.createPlanning)
          setPlanningData({
            planningId: data.createPlanning.planningId,
            daysPerWeek: data.createPlanning.daysPerWeek,
            hoursPerDay: data.createPlanning.hoursPerDay,
            monthlyBudget: data.createPlanning.monthlyBudget,
            vacationPerYear: data.createPlanning.vacationPerYear,
            valueHour: data.createPlanning.valueHour,
          });
        if (data.updatePlanning) {
          setPlanningData({
            planningId: data.updatePlanning.planningId,
            daysPerWeek: data.updatePlanning.daysPerWeek,
            hoursPerDay: data.updatePlanning.hoursPerDay,
            monthlyBudget: data.updatePlanning.monthlyBudget,
            vacationPerYear: data.updatePlanning.vacationPerYear,
            valueHour: data.updatePlanning.valueHour,
          });
        }

        toast.success(
          isNewPlanning
            ? 'Seu plano foi criado com sucesso!'
            : 'Seu plano foi atualizado com sucesso!',
        );
      }
    } catch (error) {
      console.error(error);
      toast.error('Houve algum problema ao salvar o seu plano.');
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
            <h1 className="mx-auto">Perfil</h1>
          </div>
        </Header.NavBar>
      </Header.Root>

      <div className="container animate-up delay-2 flex justify-between p-12 max-w-4xl mx-auto">
        <Aside>
          <label htmlFor="profile-upload" className="relative cursor-pointer">
            <Image
              className="border-orange-400 w-[194px] h-[194px] rounded-full border-4"
              src={profileImage}
              alt="Imagem de perfil"
              width={200}
              height={200}
            />
            <input
              id="profile-upload"
              type="file"
              accept="image/png, image/jpeg, image/jpg"
              className="hidden"
              onChange={handleImageUpload}
            />
            <div className="absolute bottom-1 right-1 bg-gray-700 p-2 rounded-full shadow-md">
              <PencilSquareIcon className="w-5 h-5 text-white" />
            </div>
          </label>
          <h2 className="text-2xl font-medium text-gray-600 text-center mt-4">
            {userData.fullName}
          </h2>
          <p className="text-center mt-4 text-gray-600">
            O valor da sua hora é <br />
            <strong className="text-xl">
              {formatCurrency(planningData?.valueHour || planningData.valueHour || 0)}
            </strong>
          </p>
          <div className="mt-10">
            {planningData.planningId === '00000000-0000-0000-0000-000000000000' ? (
              <Button.Root
                onClick={handleSubmit}
                className="bg-green-500 px-20 py-2 rounded text-[#FCFDFE] hover:bg-green-600 hover:text-gray-200"
              >
                Salvar
              </Button.Root>
            ) : (
              <Button.Root
                onClick={handleSubmit}
                className="bg-green-500 px-20 py-2 rounded text-[#FCFDFE] hover:bg-green-600 hover:text-gray-200"
              >
                Atualizar
              </Button.Root>
            )}
          </div>
        </Aside>

        <main>
          <h2 className="text-3xl font-medium text-gray-600 border-b pb-4 mb-4">
            Dados do perfil
          </h2>

          <div className="flex gap-4">
            <Input.Root>
              <Input.Label
                text="Nome completo"
                htmlFor="fullName"
                className="text-gray-500"
              />
              <Input.Content>
                <Input.Action
                  id="fullName"
                  name="fullName"
                  type="text"
                  className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
                  value={userData.fullName}
                  onChange={(e) => setUserData({ ...userData, fullName: e.target.value })}
                />
              </Input.Content>
            </Input.Root>
          </div>

          <h2 className="mt-12 text-3xl font-medium text-gray-600 border-b pb-4 mb-4">
            Planejamento
          </h2>

          <div className="grid grid-cols-2 gap-4">
            {getPlanningFields().map(({ id, label, name }) => (
              <Input.Root key={id}>
                <Input.Label
                  className="text-gray-500 font-medium text-sm"
                  htmlFor={id}
                  text={label}
                  isHtml
                />
                <Input.Content>
                  <Input.Action
                    className="px-4 py-2 border rounded-sm text-sm focus:outline-orange-400"
                    type="number"
                    id={id}
                    name={name}
                    value={planningData[name as keyof Planning] || 0}
                    onChange={handleChangeInput}
                  />
                </Input.Content>
              </Input.Root>
            ))}
          </div>
        </main>
      </div>
    </div>
  );
}

function getPlanningFields() {
  return [
    {
      id: 'monthly-budget',
      label: 'Quanto eu <br/>quero ganhar por mês?',
      name: 'monthlyBudget',
    },
    {
      id: 'hours-per-day',
      label: 'Quantas horas <br/>quero trabalhar por dia?',
      name: 'hoursPerDay',
    },
    {
      id: 'days-per-week',
      label: 'Quantos dias <br/>quero trabalhar por semana?',
      name: 'daysPerWeek',
    },
    {
      id: 'vacation-per-year',
      label: 'Quantas semanas <br/>por ano você quer tirar férias?',
      name: 'vacationPerYear',
    },
  ];
}

// Função para obter os dados iniciais de planejamento
function getInitialPlanningData(planning: Planning): Planning {
  return {
    daysPerWeek: planning.daysPerWeek || 0,
    hoursPerDay: planning.hoursPerDay || 0,
    monthlyBudget: planning.monthlyBudget || 0,
    vacationPerYear: planning.vacationPerYear || 0,
    valueHour: planning.valueHour || 0,
    planningId: planning.planningId || '00000000-0000-0000-0000-000000000000', // Valor padrão caso não exista
  };
}
