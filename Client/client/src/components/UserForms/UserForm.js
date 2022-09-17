import React, { useState, useRef } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import classes from "./UserForm.module.css";

import Select from "../UI/Input/Select";

import Card from "../UI/Card/Card";
import Input from "../UI/Input/Input";
import Button from "../UI/Button/Button";
import LoadingModal from "../UI/Modals/LoadingModal";
import InfoModal from "../UI/Modals/InfoModal";

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

  const hideModalHandler = () => {
    setDataError(null);
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
      {isLoading && <LoadingModal />}
      {dataError && (
        <InfoModal
          title={dataError.title}
          message={dataError.message}
          onConfirm={hideModalHandler}
        />
      )}
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
