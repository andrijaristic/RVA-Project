import classes from "./StudentExamList.module.css";

import Card from "../UI/Card/Card";
import StudentExamItem from "./StudentExamItem";

const StudentExamList = (props) => {
  const exams = props.items.map((item) => {
    console.log(item);
    return (
      <StudentExamItem
        key={item.id}
        id={item.id}
        name={item.name}
        subject={item.subject}
        date={item.date}
        onRemove={props.onRemove}
        onEdit={props.onEdit}
      />
    );
  });
  return (
    <section className={classes.exams}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Student exams</h1>
        </section>
        <ul>{exams}</ul>
      </Card>
    </section>
  );
};

export default StudentExamList;
