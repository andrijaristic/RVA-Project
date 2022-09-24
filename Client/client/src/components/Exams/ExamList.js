import React, { useContext } from "react";

import classes from "./ExamList.module.css";
import Card from "../UI/Card/Card";
import ExamItem from "./ExamItem";
import AuthContext from "../../store/auth-context";

const ExamList = (props) => {
  const authCtx = useContext(AuthContext);
  const registeredExams = authCtx.user.exams;
  console.log(registeredExams);
  // Function to get all registered exams. Use here to update. or in Exams.js file.

  const exams = props.items.map((item) => {
    const options = { day: "numeric", month: "long", year: "numeric" };
    const formattedDate = new Date(item.examDate).toLocaleDateString(
      "en-US",
      options
    );

    const currentDate = new Date().toLocaleDateString("en-US", options);
    const datePassed = formattedDate <= currentDate;

    let registeredToExam = null;
    if (!props.admin) {
      registeredToExam = registeredExams.some((x) => x.id === item.id);
    }

    return (
      <ExamItem
        key={item.id}
        id={item.id}
        date={item.date}
        name={item.examName}
        dateTime={formattedDate}
        onClick={registeredToExam ? props.onRemove : props.onAdd}
        onView={props.onView}
        registered={registeredToExam}
        datePassed={datePassed}
        // datePassedRegistered={datePassedForRegistered}
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
