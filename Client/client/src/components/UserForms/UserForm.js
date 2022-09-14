import React, { useState, useRef } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import classes from "./UserForm.module.css";

import Card from "../UI/Card/Card";
import Input from "../UI/Input/Input";
import Button from "../UI/Button/Button";

const UserForm = (props) => {
  const history = useHistory();
  const firstInputRef = useRef();
  const secondInputRef = useRef();
  const { isLoading, sendRequest } = useHttp();
  const [dataError, setDataError] = useState(null);

  const [isFirstInputValid, setIsFirstInputValid] = useState(false);
  const [isFirstInputTouched, setIsFirstInputTouched] = useState(false);

  const [isSecondInputValid, setIsSecondInputValid] = useState(false);
  const [isSecondInputTouched, setIsSecondInputTouched] = useState(false);

  const isFormValid = isFirstInputValid && isSecondInputValid;

  const firstInputChangeHandler = () => {
    setIsFirstInputValid(firstInputRef.current.value.trim().length >= 5);
  };

  const secondInputChangeHandler = () => {
    setIsSecondInputValid(secondInputRef.current.value.trim().length >= 5);
  };

  const firstInputBlurHandler = () => {
    setIsFirstInputTouched(true);
  };

  const secondInputBlurHandler = () => {
    setIsSecondInputTouched(true);
  };

  const submitHandler = async (event) => {
    event.preventDefault();

    const requestConfig = {
      ...props.requestConfig,
      body: props.onCreateBody({
        firstInput: firstInputRef.current.value,
        secondInput: secondInputRef.current.value,
      }),
    };

    console.log(requestConfig.body);
    const data = await sendRequest(requestConfig); // fetch(url, {}) from useHttp();
    if (data.hasError) {
      setDataError({
        title: "Error",
        message: data.errorMessage,
      });

      return;
    }

    props.dataHandler(data); //ctx.onLogin(data);
    history.replace("/");
  };

  return (
    <React.Fragment>
      {/*Backdrop i overlay modals */}
      <Card className={classes.login}>
        <form onSubmit={submitHandler}>
          <Input
            ref={firstInputRef}
            id={props.firstInput.id}
            type={props.firstInput.type}
            label={props.firstInput.label}
            isTouched={isFirstInputTouched}
            isValid={isFirstInputValid}
            onBlur={firstInputBlurHandler}
            onChange={firstInputChangeHandler}
          />
          <Input
            ref={secondInputRef}
            id={props.secondInput.id}
            type={props.secondInput.type}
            label={props.secondInput.label}
            isTouched={isSecondInputTouched}
            isValid={isSecondInputValid}
            onBlur={secondInputBlurHandler}
            onChange={secondInputChangeHandler}
          />
          <Button type="submit" disabled={!isFormValid}>
            Submit
          </Button>
        </form>
      </Card>
    </React.Fragment>
  );
};

export default UserForm;
