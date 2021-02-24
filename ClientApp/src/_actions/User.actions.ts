import { LoginModel, RegisterModel } from ".";
import {
  History,
  UserService
} from "../_services";

export const UserActions = {
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

function login(userModel:LoginModel):void {
  UserService.login(userModel).then(
    () => {
      History.push('/');
      window.location.reload();
    },
    (error) => {
      console.error(error);
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
    (error) => {
      console.error(error);
    //error handling
    }
  );
}
function logout():void {
  UserService.logout();
  History.push("/Login");
  window.location.reload();
}
async function getBalance():Promise<string|undefined> {
  try {
    const response = await UserService.getBalance();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function getState():Promise<string|undefined> {
  try {
    const response = await UserService.getState();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function openAccount():Promise<void> {
  try {
    await UserService.openAccount();
  } 
  catch(error) {
    console.error(error);
  }
}
async function closeAccount():Promise<void> {
  try {
    await UserService.closeAccount();
  } 
  catch(error) {
    console.error(error);
  }
}
async function depositToAccount(amount:number):Promise<void> {
  try {
    await UserService.depositToAccount(amount);
  } 
  catch(error) {
    console.error(error);
  }
}
async function withdrawalFromAccout(amount:number):Promise<void> {
  try {
    await UserService.withdrawalFromAccout(amount);
  } 
  catch(error) {
    console.error(error);
  }
}