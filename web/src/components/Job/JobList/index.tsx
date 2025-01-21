import { Cards } from '../Cards';

interface jobListProps {
  jobs: Job[];
}

type Job = {
  jobId?: string;
  name: string;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;
};

export function JobList({ jobs }: jobListProps) {
  return (
    <div className="py-6 space-y-2">
      {jobs.map((job) => (
        <Cards
          key={job.jobId}
          jobId={job.jobId}
          name={job.name}
          remainingDays={job.remainingDays}
          status={job.status}
          valueJob={job.valueJob}
        />
      ))}
    </div>
  );
}
