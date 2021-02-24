import {
  History,
  UserAuthApi,
  UserAccountApi
} from ".";

import {
  ApiException,
  LoginModel,
  RegisterModel,
  UserResponse,
} from "../_actions";

export const UserService = {
  login,
  logout,
  register,
  getBalance,
  getState,
  openAccount,
  closeAccount,
  depositToAccount,
  withdrawalFromAccout
};

async function login(userModel:LoginModel):Promise<void> {

  return UserAuthApi("").login(userModel).then((userResponse) => {
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
  return UserAuthApi("").register(userModel).then(() => {
    console.log("User logged in successgully");
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function getBalance(): Promise<string|undefined>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).getAccountBalance().then((response) => {
    return response.message;
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function getState(): Promise<string|undefined>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).getAccountState().then((response) => {
    return response.message;
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function closeAccount(): Promise<void>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).closeAccount().then(() => {
  //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function openAccount(): Promise<void>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).openAccount().then(() => {
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function depositToAccount(amount:number): Promise<void>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).depositMoneyToAccount(amount).then(() => {
  //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
   });
}
function withdrawalFromAccout(amount:number): Promise<void>  {
  const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
  const token = userResponse.token || "";
  return UserAccountApi(token).withdrawalMoneyFromAccount(amount).then(() => {
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    console.log(handledException);
    return Promise.reject(error);
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
