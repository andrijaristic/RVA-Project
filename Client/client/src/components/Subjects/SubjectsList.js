import classes from "./SubjectsList.module.css";

import Card from "../UI/Card/Card";
import SubjectItem from "./SubjectItem";

const SubjectsList = (props) => {
  const subjects = props.items.map((item) => {
    return (
      <SubjectItem key={item.id} id={item.id} subjectName={item.subjectName} />
    );
  });

  return (
    <section className={classes.subjects}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Subjects</h1>
        </section>
        <ul>{subjects}</ul>
      </Card>
    </section>
  );
};

export default SubjectsList;
