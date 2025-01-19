import { redirect } from 'next/navigation';
import { apiClient } from './api-client';

type User = {
  fullName: string;
  email: string;
  password: string;
};

type Login = {
  email: string;
  password: string;
};

export async function createUser(user: User) {
  const response = await apiClient.post('/users', user);
  const data = await response.data;

  return data;
}

export async function getProfile() {
  try {
    const respose = await apiClient.get('/users/me');
    const data = await respose.data;

    return data;
  } catch (error) {
    console.log('error =>', error);

    return redirect('/');
  }
}
