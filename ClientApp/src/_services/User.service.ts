import {
  History,
  UserApi
} from ".";

import {
  ApiException,
  LoginModel,
  RegisterModel,
} from "../_actions";

export const UserService = {
  login,
  logout,
  register
};

async function login(userModel:LoginModel):Promise<void> {

  return UserApi("").login(userModel).then((userResponse) => {
    console.log("User logged in successgully");
    localStorage.setItem("User", JSON.stringify(userResponse));
    return userResponse;
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return error;
  });
}

function logout():void {
  localStorage.removeItem("User");
}

function register(userModel:RegisterModel):Promise<void>  {
  return UserApi("").register(userModel).then(() => {
    console.log("User logged in successgully");
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return error;
   });
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
