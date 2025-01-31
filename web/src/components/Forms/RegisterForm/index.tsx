'use client';
import { useMutation } from '@apollo/client';
import { zodResolver } from '@hookform/resolvers/zod';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import toast from 'react-hot-toast';
import { RegisterDataProps, registerSchema } from '../../../libs/zod-schema';
import { CREATE_USER } from '../../../service/graphql/mutations/userMutation';
import { Input } from '../../Input';

export function RegisterForm() {
  const router = useRouter();
  const [registerUser] = useMutation(CREATE_USER);
  const [showPassword, setShowPassword] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterDataProps>({
    mode: 'onBlur',
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = async (data: RegisterDataProps) => {
    try {
      await registerUser({
        variables: {
          input: {
            fullName: data.fullName,
            email: data.email,
            password: data.password,
          },
        },
      });
      toast.success('Conta criada com sucesso!');
      return router.push('/sign-in');
    } catch (error) {
      return toast.error('Algo deu errado ao criar novo usuário.');
    }
  };
  return (
    <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
        <div>
          <Input.Root>
            <Input.Label
              htmlFor="full-name"
              className="block text-sm/6 font-medium text-[#FEFCFD]"
              text="Nome completo"
            />
            <Input.Content>
              <Input.Action
                {...register('fullName')}
                id="full-name"
                name="fullName"
                type="text"
                className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
              />
            </Input.Content>
            <Input.HelpText
              helpText={errors.fullName?.message}
              className="mt-2 text-orange-500"
            />
          </Input.Root>
        </div>
        <div>
          <Input.Root>
            <Input.Label
              text="Endereço de email"
              htmlFor="email"
              className="block text-sm/6 font-medium text-[#FEFCFD]"
            />
            <Input.Action
              {...register('email')}
              id="email"
              name="email"
              type="email"
              className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
            />
            <Input.HelpText
              helpText={errors.email?.message}
              className="mt-2 text-orange-500"
            />
          </Input.Root>
        </div>

        <div>
          <Input.Root>
            <Input.Label
              text="Senha"
              htmlFor="password"
              className="block text-sm/6 font-medium text-[#FEFCFD]"
            />
            <Input.Content>
              <Input.Action
                {...register('password')}
                id="password"
                type={showPassword ? 'text' : 'password'}
                className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
              />
              <Input.Eye setShowPassword={setShowPassword} showPassword={showPassword} />
            </Input.Content>
            <Input.HelpText
              helpText={errors.password?.message}
              className="mt-2 text-orange-500"
            />
          </Input.Root>
        </div>
        <div>
          <Input.Root>
            <Input.Label
              text="Confirmar senha"
              htmlFor="confirmPassword"
              className="block text-sm/6 font-medium text-[#FEFCFD]"
            />
            <Input.Content>
              <Input.Action
                {...register('confirmPassword')}
                id="confirmPassword"
                type="password"
                name="confirmPassword"
                className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
              />
            </Input.Content>
            <Input.HelpText
              helpText={errors.confirmPassword?.message}
              className="mt-2 text-orange-500"
            />
          </Input.Root>
        </div>

        <div>
          <button
            type="submit"
            className="flex w-full justify-center rounded-md bg-orange-400 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-sm hover:bg-orange-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-orange-600"
          >
            Criar
          </button>
        </div>
      </form>

      <p className="mt-10 text-center text-sm/6 text-[#FEFDFC]">
        Já possui uma conta?{' '}
        <Link
          href="/sign-in"
          className="font-semibold text-orange-400 hover:text-orange-500"
        >
          Entar
        </Link>
      </p>
    </div>
  );
}
