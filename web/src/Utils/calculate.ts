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
  const totalHours = jobs.reduce((acc, curr) => {
    return curr.status ? acc + curr.dailyHours : acc;
  }, 0);

  return planning.hoursPerDay - totalHours;
}
