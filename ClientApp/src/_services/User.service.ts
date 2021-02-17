import {
  History,
  UserApi
} from ".";

import {
  ApiException,
  LoginModel
} from "../_actions";

export const UserService = {
  login,
  logout,
};

async function login(username: string, password: string) {

  try {
    const userapi = UserApi("");

    const response = await userapi.login(
      new LoginModel({ username: username, password: password })
    );
    console.log(response);
//!!!!JSON.parse(tokenHolder).accessToken
  
    // const handledResponse = (await handleResponseBlob(
    //   response
    // )) as IUserSubscribe;
    // store user details and jwt token in local storage to keep user logged in between page refreshes
    //sessionStorage.setItem("user", JSON.stringify(handledResponse));
    //return handledResponse;
  } catch (error) {
    console.log(error);
    //const exeption = await handleExeption(error);
   // return exeption;
  }
}

function logout():void {
  localStorage.removeItem("user");
}
async function handleExeption(error: ApiException) {
  if (error.status === 401 && error.response === "Unauthorized") {
    // auto logout if 401 response returned from api
    logout();
    History.push("/Login");
  }
  if (error.response) {
    const exeptionRefuse = JSON.parse(error.response);
    return Promise.reject(exeptionRefuse.statusText);
  }
}
