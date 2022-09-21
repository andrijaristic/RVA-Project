import React from "react";

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
    <Card>
      <ul>{students}</ul>
    </Card>
  );
};

export default StudentsList;
