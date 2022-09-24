import classes from "./SubjectItem.module.css";

const SubjectItem = (props) => {
  return (
    <li className={classes.subject}>
      <section className={classes.info}>
        <p>{props.subjectName}</p>
      </section>
    </li>
  );
};

export default SubjectItem;
