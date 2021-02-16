import React, { ReactElement } from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";

interface LoginPrivateRouteProps extends RouteProps {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  component: any;
}

export const LoginPrivateRoute = (props: LoginPrivateRouteProps): ReactElement => {
  const { component: Component, ...rest } = props;
  return (
    <Route
      {...rest}
      render={(props) =>
        localStorage.getItem("user") ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{ pathname: "/Login", state: { from: props.location } }}
          />
        )
      }
    />
  );
};
