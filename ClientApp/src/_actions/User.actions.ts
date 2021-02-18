import {
  History,
  UserService
} from "../_services";

export const UserActions = {
  login,
  logout,
};

function login(
  username: string,
  password: string
):void {
  UserService.login(username, password).then(
    () => {
      History.push("/");
      //sucess handling
    },
    () => {
    //error handling
    }
  );
}
function logout():void {
  UserService.logout();
  History.push("/Login");
}