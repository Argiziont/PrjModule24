import React, { useState } from "react";
import { LoninPage } from "../LoginPage";
import { RegisterPage } from "../RegisterPage";

export const AppPage: React.FC = () => {

  const [isRegister, setIsRegister] = useState<boolean>(false);
  return (
    isRegister?<RegisterPage setIsRegister={setIsRegister}></RegisterPage>:
    <LoninPage setIsRegister={setIsRegister} ></LoninPage>
    //<h1>My React and TypeScript App!  {new Date().toLocaleDateString()}</h1>
  );
}