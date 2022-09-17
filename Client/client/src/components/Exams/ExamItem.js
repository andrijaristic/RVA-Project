import Button from "../UI/Button/Button";

const ExamItem = (props) => {
  const clickHandler = () => {
    props.onClick(props.id);
  };

  const viewHandler = () => {
    props.onView(props.id);
  };

  const dummyHandler = () => {
    console.log(`Hi :) => [${props.id}]`);
  };

  // date = true -> proslo " dugme: rezultat "
  let clickHandlerContent = dummyHandler;
  let buttonContent = "";
  //<Button onClick={clickHandlerContent}>{buttonContent}</Button>
  if (props.registered && props.date) {
    // Registrovan i prosao datum prijave.
    buttonContent = <Button onClick={clickHandlerContent}>View Results</Button>;
    clickHandlerContent = viewHandler;
  } else if (props.registered && !props.date) {
    // Registrovan i nije prosao datum prijave.
    buttonContent = <Button onClick={clickHandlerContent}>Remove</Button>;
    clickHandlerContent = clickHandler;
  } else if (!props.registered && !props.date) {
    // Nije registrovan i nije prosao datum prijave.
    buttonContent = <Button onClick={clickHandlerContent}>Apply</Button>;
    clickHandlerContent = clickHandler;
  }

  if (props.admin) {
    return (
      <li>
        <h2>{props.name}</h2>
        <p>{props.dateTime}</p>
        <Button onClick={dummyHandler}>List Students</Button>
      </li>
    );
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
