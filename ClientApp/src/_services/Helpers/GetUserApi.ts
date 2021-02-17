import { Client } from "../../_actions";
import {
  CreateAuthFetchApi,
  ApiUrl
} from "../../_services";

export const UserApi = (tokenHolder: string): Client => {
  console.log("TEST");
  return new Client(ApiUrl,
    CreateAuthFetchApi({
      Authorization: tokenHolder
    }));
};