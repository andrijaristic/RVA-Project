import React from "react";

import Card from "../UI/Card/Card";
import SubjectItem from "./SubjectItem";

const SubjectsList = (props) => {
  const subjects = props.items.map((item) => {
    return (
      <SubjectItem key={item.id} id={item.id} subjectName={item.subjectName} />
    );
  });

  return (
    <Card>
      <ul>{subjects}</ul>
    </Card>
  );
};

export default SubjectsList;
