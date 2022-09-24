import React, { useContext } from "react";

import AuthContext from "../store/auth-context";

const HomePage = () => {
  const ctx = useContext(AuthContext);
  const { user } = ctx;

  return (
    <div className="fancy">
      <p>
        {user ? "Welcome, " : "Welcome"}
        <br />
        {user && `${user.name} ${user.lastName}`}
      </p>
    </div>
  );
};

export default HomePage;
