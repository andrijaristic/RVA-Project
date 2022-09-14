import React, { useContext } from "react";

import AuthContext from "../store/auth-context";

const HomePage = () => {
  const ctx = useContext(AuthContext);
  const { user } = ctx;

  return (
    <div className="fancy">
      {user ? `${user.name} ${user.lastName}` : "Home"}
    </div>
  );
};

export default HomePage;
