import { ApolloClient, ApolloLink, HttpLink, InMemoryCache } from '@apollo/client';
import { getToken } from '../../libs/getToken';

const httpLink = new HttpLink({
  uri: `${process.env.NEXT_PUBLIC_API_URL}/graphql`,
  fetch: async function (uri, options) {
    const token = await getToken();
    return fetch(uri, {
      ...(options ?? {}),
      headers: {
        ...(options?.headers ?? {}),
        Authorization: `Bearer ${token}`,
      },
      next: {
        revalidate: 0,
      },
    });
  },
});
export const client = new ApolloClient({
  link: ApolloLink.from([httpLink]),
  cache: new InMemoryCache(),
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
