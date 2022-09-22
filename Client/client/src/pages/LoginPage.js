import React, { useContext } from "react";

import UserForm from "../components/UserForms/UserForm";
import AuthContext from "../store/auth-context";

const LoginPage = () => {
  const authCtx = useContext(AuthContext);

  const requestConfig = {
    url: "https://localhost:44344/api/Users/login",
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const firstInput = {
    id: "username",
    type: "text",
    label: "Username",
  };

  const secondInput = {
    id: "password",
    type: "password",
    label: "Password",
  };

  const createBodyHandler = (data) => {
    return JSON.stringify({
      username: data.firstInput,
      password: data.secondInput,
    });
  };

  return (
    <UserForm
      requestConfig={requestConfig}
      dataHandler={authCtx.onLogin}
      title="Login"
      firstInput={firstInput}
      secondInput={secondInput}
      onCreateBody={createBodyHandler}
    />
  );
};

export default LoginPage;
