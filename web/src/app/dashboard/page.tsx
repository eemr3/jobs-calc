import { cookies } from 'next/headers';
import { JobMain } from '../../components/Job';
import { SessionExpired } from '../../components/SessionExpired';

const baseUrl = 'http://backend:8080';

export default async function Dashboard() {
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
    const response = await fetch(`${baseUrl}/api/v1/profile`, {
      headers: {
        authorization: `Bearer ${token?.value}`,
      },
    });
    const result = await response.json();
    return result;
  }

  async function fetchJobs() {
    'use server';

    const response = await fetch(`${baseUrl}/api/v1/jobs`, {
      headers: {
        authorization: `Bearer ${token?.value}`,
      },
    });

    if (response.status === 401) return null;

    const result = await response.json();
    return result;
  }
  const user = await fetchProfile();
  const planning = await fetchPlanning();
  const jobs = await fetchJobs();

  if (user === null || jobs === null) {
    return <SessionExpired />;
  }

  return <JobMain jobsData={jobs} planningData={planning} user={user} />;
}
