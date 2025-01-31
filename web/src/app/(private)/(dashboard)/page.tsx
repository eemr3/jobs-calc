import { JobMain } from '../../../components/Job';
import {
  fetchJobs,
  fetchPlanning,
  fetchProfile,
} from '../../../service/graphql/graphql-reques';

export default async function Dashboard() {
  const user = await fetchProfile();
  const planning = await fetchPlanning();
  const jobs = await fetchJobs();

  return <JobMain jobsData={jobs} planningData={planning} user={user} />;
}
