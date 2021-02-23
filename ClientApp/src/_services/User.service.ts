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

async function login(username: string, password: string):Promise<void> {

  return UserApi("").login(new LoginModel({ username: username, password: password })).then((userResponse) => {
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
