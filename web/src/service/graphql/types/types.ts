export interface UserQueryResponse {
  me: UserQuery;
}

export interface UserQuery {
  userId: number;
  fullName: string;
  email: string;
  avatarUrl: string;
}

export interface JobsResponse {
  allJobs: Jobs[];
}

export interface Jobs {
  jobId: string;
  name: string;
  dailyHours: number;
  totalHours: number;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId: number;
}

export interface Job {
  job: {
    jobId: string;
    name: string;
    dailyHours: number;
    totalHours: number;
    createdAt: string;
    userId: number;
  };
}

export interface IJob {
  jobId: string;
  name: string;
  dailyHours: number;
  totalHours: number;
  createdAt: string;
  userId: number;
}

export interface PlanningResponse {
  planning: Planning;
}

export interface Planning {
  planningId: string;
  daysPerWeek: number;
  hoursPerDay: number;
  monthlyBudget: number;
  valueHour: number;
  vacationPerYear: number;
  userId: number;
}

export interface CreateJobInput {
  name: string;
  dailyHours: number;
  totalHours: number;
}
