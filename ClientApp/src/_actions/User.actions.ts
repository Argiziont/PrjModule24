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
) {
  return UserService.login(username, password).then(
    (user) => {
        History.push("/");
      return user;
    },
    (error) => {
      return error;
    }
  );
}
function logout() {
  UserService.logout();
  History.push("/Login");
}