import { gql } from '@apollo/client';

export const GET_PLANNING = gql`
  query GetPlanning {
    planning {
      planningId
      daysPerWeek
      hoursPerDay
      monthlyBudget
      vacationPerYear
      valueHour
      userId
    }
  }
`;
