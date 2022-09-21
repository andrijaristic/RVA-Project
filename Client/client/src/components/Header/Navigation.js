import React, { useContext } from "react";
import { NavLink } from "react-router-dom";

import classes from "./Navigation.module.css";
import AuthContext from "../../store/auth-context";
import Button from "../UI/Button/Button";

const Navigation = () => {
  const authCtx = useContext(AuthContext);
  const isAdmin =
    authCtx.user !== null &&
    authCtx.user !== undefined &&
    authCtx.user.userType === 0;

  const logoutHandler = () => {
    authCtx.onLogout();
  };

  return (
    <nav className={classes.nav}>
      <ul>
        <li>
          <NavLink activeClassName={classes.active} to="/" exact>
            Home
          </NavLink>
        </li>
        {!authCtx.isLoggedIn && (
          <li>
            <NavLink activeClassName={classes.active} to="/login">
              Login
            </NavLink>
          </li>
        )}
        {authCtx.isLoggedIn && (
          <li>
            <NavLink activeClassName={classes.active} to="/logs">
              Logs
            </NavLink>
          </li>
        )}
        {isAdmin && (
          <li>
            <NavLink activeClassName={classes.active} to="/register">
              Register
            </NavLink>
          </li>
        )}
        {authCtx.isLoggedIn && (
          <li>
            <NavLink activeClassName={classes.active} to="/edit-profile">
              Profile
            </NavLink>
          </li>
        )}
        {isAdmin && (
          <li>
            <NavLink activeClassName={classes.active} to="/students">
              Students
            </NavLink>
          </li>
        )}
        {isAdmin && (
          <li>
            <NavLink activeClassName={classes.active} to="/add-subject">
              Add Subject
            </NavLink>
          </li>
        )}
        {authCtx.isLoggedIn && (
          <li>
            <NavLink activeClassName={classes.active} to="/subjects">
              Subjects
            </NavLink>
          </li>
        )}
        {isAdmin && (
          <li>
            <NavLink activeClassName={classes.active} to="/add-exam">
              Add Exam
            </NavLink>
          </li>
        )}
        {authCtx.isLoggedIn && (
          <li>
            <NavLink activeClassName={classes.active} to="/exams">
              Exams
            </NavLink>
          </li>
        )}
        {authCtx.isLoggedIn && (
          <li>
            <Button onClick={logoutHandler}>Logout</Button>
          </li>
        )}
      </ul>
    </nav>
  );
};

export default Navigation;
