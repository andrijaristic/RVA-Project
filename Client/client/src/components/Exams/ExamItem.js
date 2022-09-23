import Button from "../UI/Button/Button";
import { Link } from "react-router-dom";

const ExamItem = (props) => {
  const clickHandler = () => {
    props.onClick(props.id, props.date);
  };

  const viewHandler = () => {
    props.onView(props.id);
  };

  if (props.admin) {
    return (
      <li>
        <h2>{props.name}</h2>
        <p>{props.dateTime}</p>
        <Link to={`/exams/${props.id}`}>Detailed view</Link>
      </li>
    );
  }

  // !Important
  // TODO: Odvratno, popravi ko boga te molim.
  let clickHandlerContent = null;
  let buttonContent = null;

  if (props.registered && props.datePassed) {
    // Registrovan i prosao datum prijave.
    clickHandlerContent = viewHandler;
    buttonContent = <Button onClick={clickHandlerContent}>View Results</Button>;
  } else if (props.registered && !props.datePassed) {
    // Registrovan i nije prosao datum prijave.
    clickHandlerContent = clickHandler;
    buttonContent = <Button onClick={clickHandlerContent}>Remove</Button>;
  } else if (!props.registered && !props.datePassed) {
    // Nije registrovan i nije prosao datum prijave.
    clickHandlerContent = clickHandler;
    buttonContent = <Button onClick={clickHandlerContent}>Apply</Button>;
  }

  return (
    <li>
      <h2>{props.name}</h2>
      <p>{props.dateTime}</p>
      {buttonContent}
    </li>
  );
};

export default ExamItem;
