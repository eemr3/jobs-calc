import { cookies } from 'next/headers';
import ProfileComponent from '../../components/Profile';
import { SessionExpired } from '../../components/SessionExpired';

const baseUrl = 'http://backend:8080';

export default async function ProfilePage() {
  const token = (await cookies()).get('access_token');
  async function fetchProfile() {
    'use server';

    const response = await fetch(`${baseUrl}/api/v1/users/me`, {
      headers: {
        authorization: `Bearer ${token?.value}`,
      },
    });

    if (response.status === 401) return null;

    const result = await response.json();
    return result;
  }

  async function fetchPlanning() {
    try {
      const response = await fetch(`${baseUrl}/api/v1/profile`, {
        headers: {
          authorization: `Bearer ${token?.value}`,
        },
      });
      const result = await response.json();
      return { ...result, statusCode: response.status };
    } catch (error: any) {
      console.error(error);
      throw { statusCode: error?.status };
    }
  }

  const user = await fetchProfile();
  const planning = await fetchPlanning();

  if (user === null || planning === null) {
    return <SessionExpired />;
  }
  return <ProfileComponent user={user} planning={planning} />;
}
