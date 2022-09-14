import React, { useState } from "react";

const AuthContext = React.createContext({
  token: "",
  user: {},
  isLoggedIn: false,
  onLogin: (data) => {},
  onLogout: () => {},
  onUpdate: (data) => {},
});

const retrieveStoredData = () => {
  const storedToken = localStorage.getItem("token");
  const storedUser = localStorage.getItem("user");

  return {
    token: storedToken,
    user: storedUser,
  };
};

export const AuthContextProvider = (props) => {
  const storedData = retrieveStoredData();
  const initToken = storedData.token;
  let initUser;

  if (storedData.token && storedData.user) {
    initUser = JSON.parse(storedData.user);
  }

  const [token, setToken] = useState(initToken);
  const [user, setUser] = useState(initUser);

  const isUserLoggedIn = token !== null && token !== undefined;

  const loginHandler = (data) => {
    setToken(data.token);
    localStorage.setItem("token", data.token);

    const loggedInUser = {
      id: data.id,
      name: data.name,
      lastName: data.lastname,
      userType: data.userType,
    };

    setUser(loggedInUser);
    localStorage.setItem("user", JSON.stringify(loggedInUser));
  };

  const logoutHandler = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem("token");
    localStorage.removeItem("user");
  };

  const updateHandler = (data) => {
    setUser((prevUser) => {
      const updateUser = {
        ...prevUser,
        name: data.name,
        lastName: data.lastName,
      };

      localStorage.setItem("user", JSON.stringify(updateUser));
      return updateUser;
    });
  };

  const contextValue = {
    token: token,
    user: user,
    isLoggedIn: isUserLoggedIn,
    onLogin: loginHandler,
    onLogout: logoutHandler,
    onUpdate: updateHandler,
  };

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
