import { useState } from "react";

import classes from "./DetailedExamItem.module.css";
import Button from "../UI/Button/Button";

const DetailedExamItem = (props) => {
  const [buttonClicked, setButtonClicked] = useState(false);

  console.log(props.id);
  const passHandler = () => {
    props.onClick(props.id, true);
    setButtonClicked(true);
  };

  const failHandler = () => {
    props.onClick(props.id, false);
    setButtonClicked(true);
  };

  return (
    <li className={classes.student}>
      <section className={classes.name}>
        <h2>{props.studentName}</h2>
      </section>
      <figure>
        <section>
          {props.touched && <p>{props.label}</p>}
          &nbsp;
          {props.touched && (
            <p
              className={`${classes.result} ${
                props.result === "Passed" ? classes.pass : classes.fail
              }`}
            >
              {props.result}
            </p>
          )}
        </section>
        {!props.touched && (
          <Button
            onClick={passHandler}
            disabled={buttonClicked}
            className={`${classes["first-button"]} ${classes.button}`}
          >
            Pass
          </Button>
        )}
        &nbsp;
        {!props.touched && (
          <Button
            onClick={failHandler}
            disabled={buttonClicked}
            className={classes.button}
          >
            Fail
          </Button>
        )}
      </figure>
    </li>
  );
};

export default DetailedExamItem;
