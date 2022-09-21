import React, { useRef } from "react";

import Button from "../UI/Button/Button";
import Input from "../UI/Input/Input";
import Select from "../UI/Input/Select";

const StudentsFilterForm = (props) => {
  const nameRef = useRef();
  const lastNameRef = useRef();
  const examRef = useRef();

  const applyFilterHandler = () => {
    props.onApply({
      name: nameRef.current.value.trim().toLowerCase(),
      lastName: lastNameRef.current.value.trim().toLowerCase(),
      exam: examRef.current.value.trim().toLowerCase(),
    });

    console.log({
      name: nameRef.current.value.trim().toLowerCase(),
      lastName: lastNameRef.current.value.trim().toLowerCase(),
      exam: examRef.current.value.trim().toLowerCase(),
    });
  };

  return (
    <React.Fragment>
      <Input
        ref={nameRef}
        id="name"
        label="First name"
        type="text"
        isValid={true}
        isTouched={true}
      />
      <Input
        ref={lastNameRef}
        id="lastName"
        label="Last name"
        type="text"
        isValid={true}
        isTouched={true}
      />
      <Select ref={examRef} id="exam" label="Exam" items={props.items} />
      <Button onClick={applyFilterHandler}>Apply</Button>
      <Button onClick={props.onRemove}>Hide filter</Button>
    </React.Fragment>
  );
};

export default StudentsFilterForm;
