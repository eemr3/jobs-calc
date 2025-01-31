import { gql } from '@apollo/client';

export const CREATE_JOB = gql`
  mutation CreateJob($input: JobDtoRequestInput!) {
    createJob(input: $input) {
      jobId
      name
      dailyHours
      totalHours
      createdAt
      userId
    }
  }
`;

export const UPDATE_JOB = gql`
  mutation UpdateJob($input: JobPatchDtoInput!, $jobId: String!) {
    updateJob(input: $input, jobId: $jobId) {
      jobId
      name
      dailyHours
      totalHours
      createdAt
      userId
    }
  }
`;

export const DELETE_JOB = gql`
  mutation DeleteJob($jobId: String!) {
    deleteJob(jobId: $jobId)
  }
`;
