import { Link } from "react-router-dom";

import Button from "../UI/Button/Button";
import classes from "./StudentItem.module.css";

const StudentItem = (props) => {
  return (
    <div>
      <section>
        <h2 className={classes.upper}>{props.studentName}</h2>
      </section>
      <Link to={`/students/${props.id}`}>
        <Button>View exams</Button>
      </Link>
    </div>
  );
};

export default StudentItem;
