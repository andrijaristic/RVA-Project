import classes from "./StudentsList.module.css";
import Card from "../UI/Card/Card";
import StudentItem from "./StudentItem";

const StudentsList = (props) => {
  const students = props.items.map((item) => {
    return (
      <StudentItem
        key={item.id}
        id={item.id}
        studentName={`${item.name} ${item.lastName}`}
        onClick={props.onClick}
      />
    );
  });

  return (
    <section className={classes.students}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Students</h1>
        </section>
        <ul>{students}</ul>
      </Card>
    </section>
  );
};

export default StudentsList;
