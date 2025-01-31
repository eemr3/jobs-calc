'use client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import { Geist, Geist_Mono } from 'next/font/google';
import { Toaster } from 'react-hot-toast';
import './globals.css';
import { ApolloProvider } from '@apollo/client';
import { client } from '../service/graphql/ApolloClient';

const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

const queryClient = new QueryClient();

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const metadata = {
    title: 'JobsCalc',
    description: 'Calculadora para freelancer',
  };
  return (
    <html lang="pt-br">
      <head>
        <title>{metadata.title}</title>
        <meta name="description" content={metadata.description} />
      </head>
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <QueryClientProvider client={queryClient}>
          <ApolloProvider client={client}>
            <Toaster position="top-right" reverseOrder={false} />
            {children}
          </ApolloProvider>
        </QueryClientProvider>
      </body>
    </html>
  );
}
