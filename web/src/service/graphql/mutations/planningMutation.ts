import { gql } from '@apollo/client';

export const CREATE_PLANNING = gql`
  mutation CreatePlanning($input: PlanningDtoRequestInput!) {
    createPlanning(input: $input) {
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

export const UPDATE_PLANNING = gql`
  mutation UpdatePlanning($input: PlanningUpdateDtoInput!) {
    updatePlanning(input: $input) {
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
