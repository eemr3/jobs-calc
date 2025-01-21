import axios from 'axios';
import { getCookie } from 'cookies-next/client';

export const apiClient = axios.create({
  baseURL: 'http://localhost:5043/api/v1',
  timeout: 5000,
});

apiClient.interceptors.request.use(
  async (config) => {
    const token = getCookie('access_token'); // Recupera o token do cookie

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);
