import { gql } from '@apollo/client';

export const GET_JOBS = gql`
  query GetJobs {
    allJobs {
      jobId
      dailyHours
      name
      remainingDays
      status
      totalHours
      valueJob
      userId
    }
  }
`;

export const GET_JOB = gql`
  query GetJob($jobId: String!) {
    job(jobId: $jobId) {
      jobId
      name
      dailyHours
      totalHours
      createdAt
      userId
    }
  }
`;
