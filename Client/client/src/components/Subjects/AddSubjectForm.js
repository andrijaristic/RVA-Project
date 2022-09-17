import React, { useState, useRef } from "react";

import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import Input from "../UI/Input/Input";

const AddSubjectForm = (props) => {
  const nameRef = useRef();
  const [isNameValid, setIsNameValid] = useState(false);
  const [isNameTouched, setIsNameTouched] = useState(false);

  const nameChangeHandler = () => {
    setIsNameValid(nameRef.current.value.trim().length !== 0);
  };
  const nameBlurHandler = () => {
    setIsNameTouched(true);
  };

  const submitHandler = (event) => {
    event.preventDefault();
    props.onSubmit({
      subjectName: nameRef.current.value,
    });
  };

  return (
    <Card>
      <section>
        <h1>{props.title}</h1>
      </section>
      <form onSubmit={submitHandler}>
        <Input
          ref={nameRef}
          type="text"
          id="name"
          label="Subject name"
          isValid={isNameValid}
          isTouched={isNameTouched}
          onBlur={nameBlurHandler}
          onChange={nameChangeHandler}
        />
        <Button type="submit" disabled={!isNameTouched}>
          Submit
        </Button>
      </form>
    </Card>
  );
};

export default AddSubjectForm;
