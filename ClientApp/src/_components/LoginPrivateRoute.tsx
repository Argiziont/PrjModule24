import React, { ReactElement } from "react";
import { useEffect } from "react";
import { useState } from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";
import {
  UserApi
} from "../_services";
interface LoginPrivateRouteProps extends RouteProps {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  component: any;
}

export const LoginPrivateRoute = (props: LoginPrivateRouteProps): ReactElement => {
  const { component: Component, ...rest } = props;

  const [isLodaing, setIsLodaing] = useState<boolean>(true);
  const [isSuccess, setisSuccess] = useState<boolean>(false);

  useEffect(() => {
    let isMounted = true;

    
       if (localStorage.getItem("User")) {
        const token = JSON.parse(localStorage.getItem("User") || '{}').token;
        UserApi(token).tryLogin().then(() => {
          if (isMounted) {
            setisSuccess(true);
            setIsLodaing(false);
          }
        }, () => {
          if (isMounted) {
            setisSuccess(false);
            setIsLodaing(false);
          }
          });
       }
       else {
         if (isMounted) {
          setisSuccess(false);
          setIsLodaing(false);}
       }
      
    return () => { isMounted = false }; // use effect cleanup to set flag false, if unmounted
  }, []);


  if (!isLodaing) {
    if (isSuccess) {
      return (
        <Route
          {...rest}
          render={(props) => <Component {...props} />
          }
        />
      );
    }
    else {
      return (
        <Route
          {...rest}
          render={(props) => {          
            return (
              <Redirect
                to={{ pathname: "/Login", state: { from: props.location } }}
              />
            );
          }}
        />
      );
    }
  }
  else {
    return (<div></div>);
  }
 
};
