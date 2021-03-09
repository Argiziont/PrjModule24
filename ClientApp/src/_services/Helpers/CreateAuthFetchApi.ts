import { IAuth } from "..";

export const CreateAuthFetchApi = (userToken: IAuth):{ fetch: (url: RequestInfo, init?: RequestInit | undefined) => Promise<Response>; } => {
  if (userToken === null) {
    //throw Error("Token must me initialized");
  }
  return {
    fetch: async (url: RequestInfo, init?: RequestInit): Promise<Response> => {
      const authAccessHeader = {
        Authorization: `Bearer ${userToken.Authorization}`,
      };
      const defaultHeaders = {
        Accept: "application/json, text/plain, */*",
        ...authAccessHeader
      };

      return fetch(url, {
        ...init,
        headers: {
          ...defaultHeaders,
          ...init?.headers,
        },
      }).then((response) => {
        if (response.status === 401) {
          localStorage.removeItem("User");
        }
        return response;
      });
    },
  };
};
