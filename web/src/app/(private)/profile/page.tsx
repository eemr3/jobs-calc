import ProfileComponent from '../../../components/Profile';
import { fetchPlanning, fetchProfile } from '../../../service/graphql/graphql-reques';

export default async function ProfilePage() {
  const user = await fetchProfile();
  const planning = await fetchPlanning();

  if (user === null || planning === null) {
    return;
  }
  return <ProfileComponent user={user} planning={planning} />;
}
