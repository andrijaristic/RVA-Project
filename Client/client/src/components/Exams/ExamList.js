import React, { useContext } from "react";

import Card from "../UI/Card/Card";
import ExamItem from "./ExamItem";
import AuthContext from "../../store/auth-context";

const ExamList = (props) => {
  const authCtx = useContext(AuthContext);
  const registeredExams = authCtx.user.exams;

  const exams = props.items.map((item) => {
    const options = { day: "numeric", month: "long", year: "numeric" };
    const date = new Date(item.examDate).toLocaleDateString("en-US", options);
    const currentDate = new Date().toLocaleTimeString("en-US", options);

    console.log(registeredExams);

    const registeredToExam = registeredExams.some((x) => x.id === item.id);
    const pastDate = registeredExams.some((x) => x.date < currentDate);

    console.log(`${item.id} | ${registeredToExam}`);

    return (
      <ExamItem
        key={item.id}
        id={item.id}
        name={item.examName}
        dateTime={date}
        onClick={registeredToExam ? props.onRemove : props.onAdd}
        onView={props.onView}
        registered={registeredToExam}
        date={pastDate}
        admin={props.admin}
      />
    );
  });

  return (
    <Card>
      <ul>{exams}</ul>
    </Card>
  );
};

export default ExamList;
