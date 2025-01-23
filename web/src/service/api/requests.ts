import { redirect } from 'next/navigation';
import { apiClient } from './api-client';
import axios from 'axios';
import { Planning } from '../../Utils/types/planning';

type User = {
  fullName: string;
  email: string;
  password: string;
};

type CreateJob = {
  name?: string;
  dailyHours?: number;
  totalHours?: number;
};

type UpdateJobData = {
  name?: string;
  dailyHours?: number;
  totalHours?: number;
};

export async function createUser(user: User) {
  try {
    const response = await apiClient.post('/users', user);
    const data = await response.data;

    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export async function getProfile() {
  try {
    const respose = await apiClient.get('/users/me');
    const data = await respose.data;

    return data;
  } catch (error) {
    if ((error as any).status === 401) {
      return redirect('/');
    }
    console.log('error =>', error);
  }
}

export async function uploadAvatar(file: any) {
  try {
    const avatarUrl = await apiClient.put('/users/upload-avatar', file);
    return avatarUrl.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      // Retorna o erro para que o componente o trate
      throw error;
    }
    throw new Error('Erro inesperado ao fazer o upload da imagem.');
  }
}

export async function getPlanning() {
  try {
    const response = await apiClient.get('/profile');
    if (response.status === 404) {
      return null;
    }
    const data = response.data;
    return { ...data, statusCode: 200 };
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export async function createPlanning(planning: Planning) {
  try {
    const response = await apiClient.post('/profile', planning);
    const data = await response.data;

    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export async function updatePlanning(planning: Planning) {
  try {
    const response = await apiClient.put('/profile', planning);
    const data = await response.data;

    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export async function getJobs() {
  try {
    const response = await apiClient.get('/jobs');
    const data = await response.data;
    if (response.status === 200) {
      return data;
    }
    return [];
  } catch (error) {
    if ((error as any).status === 401) {
      return redirect('/');
    }
    console.error('error =>', error);
  }
}

export async function createNewProject(jobData: CreateJob) {
  try {
    const response = await apiClient.post('/jobs', jobData);

    const data = response.data;

    if (response.status === 201) {
      return data;
    }
  } catch (error) {
    console.error(error);
    return error;
  }
}

export async function getJobById(jobId: string) {
  try {
    const response = await apiClient.get(`/jobs/${jobId}`);
    const data = response.data;

    return data;
  } catch (error: any) {
    if (axios.isAxiosError(error)) {
      return error;
    }
  }
}

export async function updateJob(jobId: string, jobData: UpdateJobData) {
  try {
    const response = await apiClient.put(`/jobs/${jobId}`, jobData);
    const data = response.data;
    if (response.status === 200) {
      return data;
    }

    return { message: 'Erro ao realizar a edição do projeto.' };
  } catch (error) {
    console.error(error);
  }
}

export async function deleteJob(jobId: string) {
  try {
    const response = await apiClient.delete(`/jobs/${jobId}`);

    if (response.status === 204) {
      return true;
    }

    return false;
  } catch (error) {}
}
