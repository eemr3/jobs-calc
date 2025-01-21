import { ProjectComponent } from '../../../components/Project';

import { cookies } from 'next/headers';
interface PageProps {
  params: Promise<{ id?: string }>;
}

export default async function Project({ params }: PageProps) {
  const { id } = await params;
  const isEditMode = id != 'new';

  const token = (await cookies()).get('access_token');

  async function getJob() {
    'use server';
    const response = await fetch(`http://localhost:5043/api/v1/jobs/${id}`, {
      headers: {
        authorization: `Bearer ${token?.value}`,
      },
    });
    const job = await response.json();

    return job;
  }
  async function getPlanning() {
    const planningRes = await fetch(`http://localhost:5043/api/v1/profile`, {
      headers: {
        authorization: `Bearer ${token?.value}`,
      },
    });
    const planning = await planningRes.json();

    return planning;
  }

  const planning = await getPlanning();

  const projectData = isEditMode
    ? await getJob()
    : { name: '', dailyHours: 0, totalHours: 0 };

  return (
    <ProjectComponent job={projectData} planning={planning} isEditMode={isEditMode} />
  );
}