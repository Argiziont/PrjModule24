import React, { ReactElement } from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";

interface HomePrivateRouteProps extends RouteProps {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  component: any;
}

//export  const HomePrivateRoute: React.FC<HomePrivateRouteProps> = ({ component, ...rest })=>{
export const HomePrivateRoute = (props: HomePrivateRouteProps):ReactElement => {
  const { component: Component, ...rest } = props;
  return (
    <Route
      {...rest}
      render={(props) =>
        localStorage.getItem("user") ? (
          <Redirect to={{ pathname: "/", state: { from: props.location } }} />
        ) : (
          <Component {...props} />
        )
      }
    />
  );
};
