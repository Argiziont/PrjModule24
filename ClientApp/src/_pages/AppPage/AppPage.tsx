import React, { useState } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import { LoginPrivateRoute } from "../../_components";
import { LoginPage, HomePage, RegisterPage } from "../";

export const AppPage: React.FC = () => {
  
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [isRegister, setIsRegister] = useState<boolean>(false);

  return (
    <Router>
      <Switch>
        <LoginPrivateRoute
          exact
          path="/"
          component={() => <HomePage></HomePage>}
        />
        <Route
          exact
          path="/Login"
          component={() => (
            <LoginPage setIsRegister={setIsRegister}></LoginPage>
          )}
        />
        <Route
          exact
          path="/Register"
          component={() => (
            <RegisterPage setIsRegister={setIsRegister}></RegisterPage>
          )}
        />
      </Switch>
    </Router>
  );
};
