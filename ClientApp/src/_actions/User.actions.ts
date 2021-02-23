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
      History.push('/');
      window.location.reload();
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