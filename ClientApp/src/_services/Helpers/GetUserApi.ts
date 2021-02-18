import { AuthenticateClient } from "../../_actions";
import {
  CreateAuthFetchApi,
  ApiUrl
} from "../../_services";

export const UserApi = (tokenHolder: string): AuthenticateClient => {
  return new AuthenticateClient(ApiUrl,
    CreateAuthFetchApi({
      Authorization: tokenHolder
    }));
};