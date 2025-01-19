import { z } from 'zod';

export const schema = z.object({
  email: z.string().email('Endereço de email não esta no padrão válido.'),
  password: z.string().min(6, 'A senha precisa ter no mínimo 6 caracteres'),
});

export type DataProps = z.infer<typeof schema>;
