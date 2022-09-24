import classes from "./DetailedExamList.module.css";

import Card from "../UI/Card/Card";
import DetailedExamItem from "./DetailedExamItem";

const DetailedExamList = (props) => {
  const students = props.items.map((item) => {
    return (
      <DetailedExamItem
        key={Math.random()}
        id={item.student.id}
        studentName={`${item.student.name} ${item.student.lastName}`}
        label={props.label}
        result={item.result ? "Passed" : "Failed"}
        onClick={props.onClick}
        touched={item.isTouched}
      ></DetailedExamItem>
    );
  });

  return (
    <section className={classes.exams}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Registered students</h1>
        </section>
        <ul>{students}</ul>
      </Card>
    </section>
  );
};

export default DetailedExamList;
