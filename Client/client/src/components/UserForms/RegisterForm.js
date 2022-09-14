import React, { useReducer, useRef, useContext, useState } from "react";
import { useHistory } from "react-router-dom";

import classes from "./RegisterForm.module.css";

import AuthContext from "../../store/auth-context";
import Card from "../UI/Card/Card";
import Input from "../UI/Input/Input";
import Button from "../UI/Button/Button";
import useHttp from "../../hooks/use-http";

const USER_TYPES = [
  {
    id: 1,
    type: "STUDENT",
  },
  { id: 2, type: "ADMIN" },
];

const initInputState = {
  isUsernameValid: false,
  isPasswordValid: false,
  isNameValid: false,
  isLastNameValid: false,
  isUsernameTouched: false,
  isPasswordTouched: false,
  isNameTouched: false,
  isLastNameTouched: false,
};

const isFiveChars = (data) => {
  return data.trim().length >= 5;
};

const inputReducer = (state, action) => {
  if (action.type === "USERNAME_CHANGE") {
    return { ...state, isUsernameValid: action.value };
  }

  if (action.type === "PASSWORD_CHANGE") {
    return { ...state, isPasswordValid: action.value };
  }

  if (action.type === "NAME_CHANGE") {
    return { ...state, isNameValid: action.value };
  }

  if (action.type === "LASTNAME_CHANGE") {
    return { ...state, isLastNameValid: action.value };
  }
  //------------------------------------------------------
  //------------------------------------------------------
  if (action.type === "USERNAME_BLUR") {
    return { ...state, isUsernameTouched: true };
  }

  if (action.type === "PASSWORD_BLUR") {
    return { ...state, isPasswordTouched: true };
  }

  if (action.type === "NAME_BLUR") {
    return { ...state, isNameTouched: true };
  }

  if (action.type === "LASTNAME_BLUR") {
    return { ...state, isLastNameTouched: true };
  }

  return initInputState;
};

const RegisterForm = () => {
  const authCtx = useContext(AuthContext);
  const history = useHistory();
  const { isLoading, sendRequest } = useHttp();

  // const [infoData, setInfoData] = useState(null);
  const [inputState, dispatchInputState] = useReducer(
    inputReducer,
    initInputState
  );

  const usernameRef = useRef();
  const passwordRef = useRef();
  const nameRef = useRef();
  const lastNameRef = useRef();
  const userTypeRef = useRef();

  const isFormValid =
    inputState.isUsernameValid &&
    inputState.isPasswordValid &&
    inputState.isNameValid &&
    inputState.isLastNameValid;

  const usernameChangeHandler = () => {
    dispatchInputState({
      type: "USERNAME_CHANGE",
      value: isFiveChars(usernameRef.current.value),
    });
  };

  const passwordChangeHandler = () => {
    dispatchInputState({
      type: "PASSWORD_CHANGE",
      value: isFiveChars(passwordRef.current.value),
    });
  };

  const nameChangeHandler = () => {
    dispatchInputState({
      type: "NAME_CHANGE",
      value: isFiveChars(nameRef.current.value),
    });
  };

  const lastNameChangeHandler = () => {
    dispatchInputState({
      type: "LASTNAME_CHANGE",
      value: isFiveChars(lastNameRef.current.value),
    });
  };

  const usernameBlurHandler = () => {
    dispatchInputState({
      type: "USERNAME_BLUR",
    });
  };

  const passwordBlurHandler = () => {
    dispatchInputState({
      type: "PASSWORD_BLUR",
    });
  };

  const nameBlurHandler = () => {
    dispatchInputState({
      type: "NAME_BLUR",
    });
  };

  const lastNameBlurHandler = () => {
    dispatchInputState({
      type: "LASTNAME_BLUR",
    });
  };

  const submitHandler = async (event) => {
    event.preventDefault();

    const requestConfig = {
      url: "https://localhost:44344/api/Users/register",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authCtx.token}`,
      },
      body: JSON.stringify({
        username: usernameRef.current.value,
        password: passwordRef.current.value,
        name: nameRef.current.value,
        lastName: lastNameRef.current.value,
        userType: USER_TYPES[1],
      }),
    };

    const data = await sendRequest(requestConfig);
    // ErrorDTO i SuccessDTO koji nedostaju na Backend-u.
    // setInfoData({
    //   title: data.hasError ? "Error" : "Success",
    //   message: data.hasError ? data.message : "User successfully added.",
    // });

    history.replace("/login");
  };

  return (
    <React.Fragment>
      {/*Backdrop i overlay modals */}
      <Card className={classes.register}>
        <form onSubmit={submitHandler}>
          <Input
            ref={usernameRef}
            id="username"
            type="text"
            label="Username"
            isTouched={inputState.isUsernameTouched}
            isValid={inputState.isUsernameValid}
            onBlur={usernameBlurHandler}
            onChange={usernameChangeHandler}
          />
          <Input
            ref={passwordRef}
            id="password"
            type="password"
            label="Password"
            isTouched={inputState.isPasswordTouched}
            isValid={inputState.isPasswordValid}
            onBlur={passwordBlurHandler}
            onChange={passwordChangeHandler}
          />
          <Input
            ref={nameRef}
            type="text"
            id="name"
            label="Name"
            isTouched={inputState.isNameTouched}
            isValid={inputState.isNameValid}
            onBlur={nameBlurHandler}
            onChange={nameChangeHandler}
          />
          <Input
            ref={lastNameRef}
            type="text"
            id="lastName"
            label="Last name"
            isTouched={inputState.isLastNameTouched}
            isValid={inputState.isLastNameValid}
            onBlur={lastNameBlurHandler}
            onChange={lastNameChangeHandler}
          />
          {/*Select komponenta za UserType */}
          <Button type="submit" disabled={!isFormValid}>
            Submit
          </Button>
        </form>
      </Card>
    </React.Fragment>
  );
};

export default RegisterForm;
