import { useState, useRef } from "react";

import classes from "./StudentExamItem.module.css";
import Button from "../UI/Button/Button";
import Input from "../UI/Input/Input";

const StudentExamItem = (props) => {
  const nameRef = useRef();
  const [editMode, setEditMode] = useState(false);

  const removeHandler = () => {
    props.onRemove(props.id);
  };

  const modeChangeHandler = () => {
    setEditMode((prevState) => !prevState);
  };

  const editHandler = () => {
    setEditMode(false);
    props.onEdit({ id: props.id, name: nameRef.current.value });
  };

  return (
    <li className={classes.exam}>
      <section className={classes.info}>
        {!editMode && <h2>{props.name}</h2>}
        {editMode && (
          <Input
            id={props.name}
            ref={nameRef}
            isValid={true}
            isTouched={true}
            name={props.name}
          />
        )}
        <p>{props.date}</p>
      </section>
      <figure>
        {!editMode && (
          <Button onClick={removeHandler} className={classes["first-button"]}>
            Remove
          </Button>
        )}
        &nbsp;
        {!editMode && <Button onClick={modeChangeHandler}>Edit</Button>}
        {editMode && (
          <Button onClick={editHandler} className={classes["first-button"]}>
            Submit changes
          </Button>
        )}
        &nbsp;
        {editMode && <Button onClick={modeChangeHandler}>Cancel</Button>}
      </figure>
    </li>
  );
};

export default StudentExamItem;
