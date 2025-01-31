import { ProjectComponent } from '../../../../components/Project';
import { fetchJobID, fetchPlanning } from '../../../../service/graphql/graphql-reques';

interface PageProps {
  params: Promise<{ id?: string }>;
}

export default async function Project({ params }: PageProps) {
  const { id } = await params;
  if (id === null || !id) throw new Error('Id não encontrado');

  const isEditMode = id != 'new';

  const planning = await fetchPlanning();

  const projectData = isEditMode
    ? await fetchJobID(id)
    : { jobId: '', name: '', dailyHours: 0, totalHours: 0, createdAt: '', userId: 0 };

  if (projectData === null || planning === null) {
    return;
  }

  return (
    <ProjectComponent job={projectData} planning={planning} isEditMode={isEditMode} />
  );
}
