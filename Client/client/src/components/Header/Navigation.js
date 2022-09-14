import React, { useContext } from "react";
import classes from "./Navigation.module.css";

import { NavLink } from "react-router-dom";

import AuthContext from "../../store/auth-context";

const Navigation = () => {
  const authCtx = useContext(AuthContext);
  return (
    <nav className={classes.nav}>
      <ul>
        <li>
          <NavLink activeClassName={classes.active} to="/" exact>
            Home
          </NavLink>
        </li>
        <li>
          <NavLink activeClassName={classes.active} to="/login">
            Login
          </NavLink>
        </li>
        <li>
          <NavLink activeClassName={classes.active} to="/register">
            Register
          </NavLink>
        </li>
        <li>
          {authCtx.isLoggedIn && (
            <NavLink activeClassName={classes.active} to="/edit-profile">
              Profile
            </NavLink>
          )}
        </li>
      </ul>
    </nav>
  );
};

export default Navigation;
