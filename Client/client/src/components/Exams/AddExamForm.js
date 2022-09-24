import React, { useState, useRef } from "react";

import classes from "./AddExamForm.module.css";
import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import Input from "../UI/Input/Input";
import Select from "../UI/Input/Select";
import DatePicker from "../UI/Input/DatePicker";

const AddExamForm = (props) => {
  const nameRef = useRef();
  const subjectRef = useRef();
  const dateTimeRef = useRef();

  const date = new Date();

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
      name: nameRef.current.value,
      subjectName: subjectRef.current.value,
      date: dateTimeRef.current.value,
    });
  };

  return (
    <Card className={classes["new-exam"]}>
      <section className={classes.title}>
        <h2>{props.title}</h2>
      </section>
      <form onSubmit={submitHandler}>
        <Input
          ref={nameRef}
          type="text"
          id="name"
          label="Exam name"
          isValid={isNameValid}
          isTouched={isNameTouched}
          onBlur={nameBlurHandler}
          onChange={nameChangeHandler}
        />
        <DatePicker
          ref={dateTimeRef}
          id="date"
          type="date"
          label="Exam date: "
          minDate={date}
        />
        <Select
          ref={subjectRef}
          id="subject"
          label="Subject: "
          items={props.items}
        />
        <Button type="submit" disabled={!isNameValid}>
          Submit
        </Button>
      </form>
    </Card>
  );
};

export default AddExamForm;
