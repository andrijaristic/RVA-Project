import React, { useContext } from "react";

import classes from "./ExamList.module.css";
import Card from "../UI/Card/Card";
import ExamItem from "./ExamItem";
import AuthContext from "../../store/auth-context";

const ExamList = (props) => {
  const authCtx = useContext(AuthContext);
  const registeredExams = authCtx.user.exams;
  const options = {
    day: "numeric",
    month: "long",
    year: "numeric",
    hours: "numeric",
    minutes: "numeric",
    seconds: "numeric",
  };

  const exams = props.items.map((item) => {
    const formattedDate = new Date(item.examDate).toLocaleTimeString(
      "en-US",
      options
    );
    const formattedDateTime = new Date(item.examDate).getTime();

    // const currentDate = new Date().toLocaleTimeString("en-US", options);
    const currentDateTime = new Date().getTime();

    const canRegister = formattedDateTime > currentDateTime;
    let registeredToExam = null;
    if (!props.admin) {
      registeredToExam = registeredExams.some((x) => x.id === item.id);
    }

    // console.log(item);
    return (
      <ExamItem
        key={item.id}
        id={item.id}
        date={item.date}
        name={item.examName}
        subject={item.subject.subjectName}
        dateTime={formattedDate}
        onClick={registeredToExam ? props.onRemove : props.onAdd}
        onView={props.onView}
        registered={registeredToExam}
        canRegister={canRegister}
        admin={props.admin}
      />
    );
  });

  return (
    <section className={classes.exams}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Exams</h1>
        </section>
        <ul>{exams}</ul>
      </Card>
    </section>
  );
};

export default ExamList;
