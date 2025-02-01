import { ApolloClient, ApolloLink, HttpLink, InMemoryCache } from '@apollo/client';
import createUploadLink from 'apollo-upload-client/createUploadLink.mjs';
import { getToken } from '../../libs/getToken';
import { setContext } from '@apollo/client/link/context';

const baseUrl = `${process.env.NEXT_PUBLIC_API_URL}/graphql`;

const uploadLink = createUploadLink({
  uri: baseUrl,
});

const authLink = setContext(async (_, { headers }) => {
  const token = await getToken();
  return {
    headers: {
      ...headers,
      authorization: token ? `Bearer ${token}` : '',
    },
  };
});

const link = ApolloLink.from([authLink, uploadLink]);

export const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: link,
  defaultOptions: {
    query: {
      fetchPolicy: 'no-cache',
      errorPolicy: 'all',
    },
    watchQuery: {
      fetchPolicy: 'no-cache',
      errorPolicy: 'all',
    },
  },
});
