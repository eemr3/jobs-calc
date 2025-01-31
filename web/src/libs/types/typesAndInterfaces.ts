import { IJob, Jobs } from '../../service/graphql/types/types';

export interface JobData {
  job: IJob;
  planning: Planning;
  isEditMode: boolean;
}

export type JobMainProps = {
  jobsData: Jobs[];
  planningData: Planning;
  user: User;
};

export type Job = {
  jobId?: string;
  name: string;
  dailyHours: number;
  totalHours?: number;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;
};

export type Planning = {
  planningId: string;
  monthlyBudget: number;
  daysPerWeek: number;
  hoursPerDay: number;
  vacationPerYear: number;
  valueHour: number;
};

export interface CardsProps {
  jobId?: string;
  name: string;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;
  onDelete: (jobId: string) => void;
}

export type ProfileProps = {
  user: User;
  planning: Planning;
};

export type UserProps = {
  user: User;
};

export type User = {
  userId: number;
  fullName: string;
  email: string;
  avatarUrl: string;
};
