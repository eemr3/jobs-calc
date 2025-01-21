type Jobs = {
  jobId?: string;
  name: string;
  dailyHours: number;
  totalHours?: number;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;
};

type Planning = {
  planningId: string;
  monthlyBudget: number;
  daysPerWeek: number;
  hoursPerDay: number;
  vacationPerYear: number;
  valueHour: number;
};

export function calculateFreeHours(jobs: Jobs[], planning: Planning) {
  if (jobs.length === 0) {
    return 0;
  }
  const totalHours = jobs?.reduce((acc, curr) => {
    return curr.status ? acc + curr.dailyHours : acc;
  }, 0);

  const value = planning.hoursPerDay - totalHours;
  return value > 0 ? value : 0;
}

export function countProjects(jobs: Jobs[]) {
  const totalTjobs = jobs.length;
  const jobsDone = jobs.filter((job) => job.status === true).length;
  const jobsfinish = jobs.filter((job) => job.status === false).length;

  return { totalTjobs, jobsDone, jobsfinish };
}
