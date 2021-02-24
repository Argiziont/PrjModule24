import { LoginModel, RegisterModel } from ".";
import {
  History,
  UserService
} from "../_services";

export const UserActions = {
  login,
  logout,
  register
};

function login(userModel:LoginModel):void {
  UserService.login(userModel).then(
    () => {
      History.push('/');
      window.location.reload();
    },
    () => {
    //error handling
    }
  );
}
function register(userModel:RegisterModel):void {
  UserService.register(userModel).then(
    () => {
      login(new LoginModel({
        password: userModel.password,
        username: userModel.username
      }))
    
    },
    () => {
    //error handling
    }
  );
}
function logout():void {
  UserService.logout();
  History.push("/Login");
  window.location.reload();
}