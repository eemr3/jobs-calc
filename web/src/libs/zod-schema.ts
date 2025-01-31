import { z } from 'zod';

export const schema = z.object({
  email: z.string().email('Endereço de email não esta no padrão válido.'),
  password: z.string().min(6, 'A senha precisa ter no mínimo 6 caracteres'),
});
export type DataProps = z.infer<typeof schema>;

const passwordValidation = new RegExp(
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/,
);
export const registerSchema = z
  .object({
    fullName: z
      .string()
      .min(3, 'O nome precisa ter no mínimo 3 caracteres')
      .max(100, 'O nome pode ter no máximo 100 caracteres'),
    email: z.string().email('O email dever ser um email válido'),
    password: z
      .string()
      .min(1, 'Senhá é obrigatória')
      .regex(passwordValidation, { message: 'Asenha é fraca' }),
    confirmPassword: z.string().min(1, 'Confirmar senha é obrigatório'),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: 'As senhas não correspondem',
    path: ['confirmPassword'],
  });

export type RegisterDataProps = z.infer<typeof registerSchema>;
