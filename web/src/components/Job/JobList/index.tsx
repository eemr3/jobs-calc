import { Cards } from '../Cards';

interface jobListProps {
  jobs: Job[];
}

type Job = {
  id: number;
  name: string;
  remainingDays: number;
  status: boolean;
  totalValue: number;
};

export function JobList({ jobs }: jobListProps) {
  return (
    <div className="py-6 space-y-2">
      {jobs.map((job) => (
        <Cards
          key={job.id}
          name={job.name}
          remainingDays={job.remainingDays}
          status={job.status}
          totalValue={job.totalValue}
        />
      ))}
    </div>
  );
}
