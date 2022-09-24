import { Link } from "react-router-dom";

import Button from "../UI/Button/Button";
import classes from "./StudentItem.module.css";

const StudentItem = (props) => {
  const clickHandler = () => {
    props.onClick(props.id);
  };

  return (
    <li className={classes.item}>
      <section>
        <h2 className={classes.name}>{props.studentName}</h2>
      </section>
      <figure>
        <Link to={`/students/${props.id}`} className={classes["first-button"]}>
          <Button>View exams</Button>
        </Link>
        &nbsp;
        <Button onClick={clickHandler}>Duplicate</Button>
      </figure>
    </li>
  );
};

export default StudentItem;
