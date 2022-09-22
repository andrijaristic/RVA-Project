import { useState, useRef } from "react";

import Button from "../UI/Button/Button";
import Card from "../UI/Card/Card";
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
    <Card>
      <section>
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
      </section>
      <div>
        <figure>
          <label htmlFor="date">Exam date</label>
          <p id="date">{props.date}</p>
        </figure>
        {!editMode && <Button onClick={removeHandler}>Remove</Button>}
        {!editMode && <Button onClick={modeChangeHandler}>Edit</Button>}
        {editMode && <Button onClick={editHandler}>Submit changes</Button>}
        {editMode && <Button onClick={modeChangeHandler}>Cancel</Button>}
      </div>
    </Card>
  );
};

export default StudentExamItem;
