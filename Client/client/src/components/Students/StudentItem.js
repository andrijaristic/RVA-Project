import React from "react";

import Button from "../UI/Button/Button";
import classes from "./StudentItem.module.css";

const StudentItem = (props) => {
  return (
    <div>
      <section>
        <h2 className={classes.upper}>{props.studentName}</h2>
      </section>
      <Button onClick={props.onClick}>View exams</Button>
    </div>
  );
};

export default StudentItem;
