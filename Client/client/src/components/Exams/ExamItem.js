import Button from "../UI/Button/Button";
import { Link } from "react-router-dom";
import classes from "./ExamItem.module.css";

const ExamItem = (props) => {
  const clickHandler = () => {
    props.onClick(props.id, props.date);
  };

  const viewHandler = () => {
    props.onView(props.id);
  };

  if (props.admin) {
    return (
      <li className={classes.exam}>
        <section className={classes.info}>
          <p className={classes.subject}>{props.subject}</p>
          <h2>{props.name}</h2>
          <p>{props.dateTime}</p>
        </section>
        <figure>
          <Link to={`/exams/${props.id}`}>
            <Button>Detailed view</Button>
          </Link>
        </figure>
      </li>
    );
  }

  let clickHandlerContent = null;
  let buttonContent = null;

  if (props.registered && !props.canRegister) {
    // Registrovan i prosao datum prijave.
    clickHandlerContent = viewHandler;
    buttonContent = (
      <Button onClick={clickHandlerContent} className={classes.button}>
        View Results
      </Button>
    );
  } else if (props.registered && props.canRegister) {
    // Registrovan i nije prosao datum prijave.
    clickHandlerContent = clickHandler;
    buttonContent = (
      <Button onClick={clickHandlerContent} className={classes.button}>
        Remove
      </Button>
    );
  } else if (!props.registered && props.canRegister) {
    // Nije registrovan i nije prosao datum prijave.
    clickHandlerContent = clickHandler;
    buttonContent = (
      <Button onClick={clickHandlerContent} className={classes.button}>
        Apply
      </Button>
    );
  }

  return (
    <li className={classes.exam}>
      <section className={classes.info}>
        <h2>{props.name}</h2>
        <p>{props.dateTime}</p>
      </section>
      <figure>{buttonContent}</figure>
    </li>
  );
};

export default ExamItem;
