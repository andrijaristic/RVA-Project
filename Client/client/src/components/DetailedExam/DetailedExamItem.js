import { useState } from "react";

import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";

const DetailedExamItem = (props) => {
  const [buttonClicked, setButtonClicked] = useState(false);

  const passHandler = () => {
    props.onClick(props.id, true);
    setButtonClicked(true);
  };

  const failHandler = () => {
    props.onClick(props.id, false);
    setButtonClicked(true);
  };

  return (
    <Card>
      <section>
        <h2>{props.studentName}</h2>
      </section>
      <div>
        <figure>
          <blockquote>
            <p>{props.label}</p>
          </blockquote>
          <figcaption>{props.result}</figcaption>
        </figure>

        {!props.touched && (
          <Button onClick={passHandler} disabled={buttonClicked}>
            Pass
          </Button>
        )}
        {!props.touched && (
          <Button onClick={failHandler} disabled={buttonClicked}>
            Fail
          </Button>
        )}
      </div>
    </Card>
  );
};

export default DetailedExamItem;
