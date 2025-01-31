import { client } from './ApolloClient';
import { GET_JOB, GET_JOBS } from './queries/jobQuery';
import { GET_PLANNING } from './queries/planningQuery';
import { GET_USER } from './queries/userQuery';
import { Job, JobsResponse, PlanningResponse, UserQueryResponse } from './types/types';

export async function fetchProfile() {
  'use server';
  try {
    const { data, errors } = await client.query<UserQueryResponse>({
      query: GET_USER,
    });

    const err = errors?.find((e) => e.extensions);
    if (err?.extensions?.code === 'AUTH_NOT_AUTHENTICATED') {
      throw new Error('Não autenticado');
    }

    return data.me;
  } catch (error: any) {
    console.error('Error ----> ', error);
    throw error;
  }
}

export async function fetchPlanning() {
  'use server';
  try {
    const { data, errors } = await client.query<PlanningResponse>({
      query: GET_PLANNING,
    });

    const err = errors?.find((e) => e.extensions);
    if (err?.extensions?.code === 'AUTH_NOT_AUTHENTICATED') {
      throw new Error('Não autenticado');
    }

    return data.planning;
  } catch (error) {
    console.error('Error ----> ', error);

    throw error;
  }
}

export async function fetchJobs() {
  'use server';
  try {
    const { data, errors } = await client.query<JobsResponse>({
      query: GET_JOBS,
    });

    const err = errors?.find((e) => e.extensions);
    if (err?.extensions?.code === 'AUTH_NOT_AUTHENTICATED') {
      throw new Error('Não autenticado');
    }

    return data.allJobs;
  } catch (error) {
    console.error('Error ----> ', error);

    throw error;
  }
}

export async function fetchJobID(jobId: string) {
  'use server';
  try {
    const { data, errors } = await client.query<Job>({
      query: GET_JOB,
      variables: { jobId },
    });

    const err = errors?.find((e) => e.extensions);
    if (err?.extensions?.code === 'AUTH_NOT_AUTHENTICATED') {
      throw new Error('Não autenticado');
    }

    return data.job;
  } catch (error) {
    console.error('Error ----> ', error);

    throw error;
  }
}
