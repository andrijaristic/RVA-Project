import React, { useContext } from "react";
import classes from "./Navigation.module.css";

import { NavLink } from "react-router-dom";

import AuthContext from "../../store/auth-context";

const Navigation = () => {
  const authCtx = useContext(AuthContext);
  const isAdmin =
    authCtx.user !== null &&
    authCtx.user !== undefined &&
    authCtx.user.userType === 0;

  return (
    <nav className={classes.nav}>
      <ul>
        <li>
          <NavLink activeClassName={classes.active} to="/" exact>
            Home
          </NavLink>
        </li>
        <li>
          {!authCtx.isLoggedIn && (
            <NavLink activeClassName={classes.active} to="/login">
              Login
            </NavLink>
          )}
        </li>
        <li>
          {!authCtx.isLoggedIn && (
            <NavLink activeClassName={classes.active} to="/register">
              Register
            </NavLink>
          )}
        </li>
        <li>
          {authCtx.isLoggedIn && (
            <NavLink activeClassName={classes.active} to="/edit-profile">
              Profile
            </NavLink>
          )}
        </li>
        <li>
          {isAdmin && (
            <NavLink activeClassName={classes.active} to="/add-subject">
              Add Subject
            </NavLink>
          )}
        </li>
        <li>
          <NavLink activeClassName={classes.active} to="/subjects">
            Subjects
          </NavLink>
        </li>
        <li>
          {isAdmin && (
            <NavLink activeClassName={classes.active} to="/add-exam">
              Add Exam
            </NavLink>
          )}
        </li>
        <li>
          {authCtx.isLoggedIn && (
            <NavLink activeClassName={classes.active} to="/exams">
              Exams
            </NavLink>
          )}
        </li>
      </ul>
    </nav>
  );
};

export default Navigation;
