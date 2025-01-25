import NextAuth from 'next-auth';
import CredentialsProvider from 'next-auth/providers/credentials';
import { cookies } from 'next/headers';

const handler = NextAuth({
  pages: {
    signIn: '/',
    signOut: '/',
  },
  providers: [
    CredentialsProvider({
      name: 'Credentials',

      credentials: {
        email: {},
        password: {},
      },
      async authorize(credentials) {
        if (!credentials) return null;

        try {
          // Faça a requisição para sua API externa
          const response = await fetch(`http://backend:8080/api/v1/auth/login`, {
            method: 'POST',
            body: JSON.stringify(credentials),
            headers: { 'Content-Type': 'application/json' },
          });

          if (response.status !== 200) return null;

          const authData = await response.json();

          // Verifique se o token foi retornado
          if (authData.access_token) {
            (await cookies()).set('access_token', authData.access_token);

            return authData;
          }
          return null; // Retorna null se as credenciais estiverem incorretas
        } catch (error) {
          console.error('Login error:', error);

          // Falha na autenticação
        }
      },
    }),
  ],
});

export { handler as GET, handler as POST };
