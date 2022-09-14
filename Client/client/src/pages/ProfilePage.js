import React, { useContext } from "react";

import AuthContext from "../store/auth-context";
import UserForm from "../components/UserForms/UserForm";

const ProfilePage = () => {
  const authCtx = useContext(AuthContext);
  const requestConfig = {
    url: "https://localhost:44344/api/Users",
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${authCtx.token}`,
    },
  };

  const firstInput = {
    id: "name",
    type: "text",
    label: "Name",
  };

  const secondInput = {
    id: "lastName",
    type: "text",
    label: "Last name",
  };

  const createBodyHandler = (data) => {
    return JSON.stringify({
      id: authCtx.user.id,
      username: "",
      name: data.firstInput,
      lastName: data.secondInput,
    });
  };

  return (
    <UserForm
      requestConfig={requestConfig}
      dataHandler={authCtx.onLogin}
      firstInput={firstInput}
      secondInput={secondInput}
      onCreateBody={createBodyHandler}
    />
  );
};

export default ProfilePage;
