import { gql } from '@apollo/client';

export const CREATE_USER = gql`
  mutation CreateUser($input: UserDtoRequestInput!) {
    registerUser(input: $input) {
      userId
      fullName
      email
    }
  }
`;

export const UPDATE_USER = gql`
  mutation UpdateUser($input: UserPatchDtoInput!) {
    updateUser(input: $input) {
      userId
      fullName
      email
    }
  }
`;
