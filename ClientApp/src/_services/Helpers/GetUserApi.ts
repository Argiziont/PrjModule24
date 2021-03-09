import {
  AccountClient,
  AuthenticateClient,
} from "../../_actions";
import {
  CreateAuthFetchApi,
  ApiUrl,
  AuthUrl
} from "../../_services";

export const UserAuthApi = (tokenHolder: string): AuthenticateClient => {
  return new AuthenticateClient(AuthUrl,
    CreateAuthFetchApi({
      Authorization: tokenHolder
    }));
};
export const UserAccountApi = (tokenHolder: string): AccountClient => {
  return new AccountClient(ApiUrl,
    CreateAuthFetchApi({
      Authorization: tokenHolder
    }));
};