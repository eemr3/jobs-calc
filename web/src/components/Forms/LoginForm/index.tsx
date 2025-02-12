'use client';
import { zodResolver } from '@hookform/resolvers/zod';
import { signIn } from 'next-auth/react';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import toast from 'react-hot-toast';
import { DataProps, schema } from '../../../Utils/zod-schema';
import { Button } from '../../Button';
import { Input } from '../../Input';
import { Icons } from '../../Icons';

export default function LoginForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const router = useRouter();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<DataProps>({
    mode: 'onBlur',
    resolver: zodResolver(schema),
  });

  const onSubmit = async (data: DataProps) => {
    setIsLoading(true);
    const result = await signIn('credentials', {
      ...data,
      redirect: false,
    });

    if (result?.error) {
      setIsLoading(false);
      toast.error('O endereço de e-mail ou a senha fornecidos estão incorretos!');
    } else {
      router.push('/dashboard');
    }
  };
  return (
    <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
        <div>
          <Input.Root>
            <Input.Label
              htmlFor="email"
              className="block text-sm/6 font-medium text-[#FEFCFD]"
              text="Endereço de email"
            />
            <Input.Content>
              <Input.Action
                {...register('email')}
                id="email"
                name="email"
                type="email"
                autoComplete="email"
                className="block w-full rounded-md bg-[#FEFDFC] px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-orange-400 sm:text-sm/6"
              />
            </Input.Content>
            <Input.HelpText
              helpText={errors?.email?.message}
              className="mt-2 text-orange-500"
            />
          </Input.Root>

          <Input.Root>
            <div className="flex items-center justify-between">
              <Input.Label
                htmlFor="password"
                className="block text-sm/6 font-medium text-[#FEFCFD]"
                text="Sua senha"
              />
              <div className="text-sm">
                <a
                  href="#"
                  className="font-semibold text-orange-400 hover:text-orange-500"
                >
                  Esqueceu a senha?
                </a>
              </div>
            </div>
            <Input.Content>
              <Input.Action
                {...register('password')}
                id="password"
                name="password"
                type={showPassword ? 'text' : 'password'}
                autoComplete="current-password"
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
          <Button.Root
            type="submit"
            className="flex w-full justify-center rounded-md bg-orange-400 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-sm hover:bg-orange-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-orange-600"
          >
            {isLoading ? <Icons.Spinner /> : 'Entrar'}
          </Button.Root>
        </div>
      </form>
      <p className="mt-10 text-center text-sm/6 text-[#FEFDFC]">
        Ainda não tem conta?{' '}
        <Link
          href="/register"
          className="font-semibold text-orange-400 hover:text-orange-500"
        >
          Crie um conta
        </Link>
      </p>
    </div>
  );
}
