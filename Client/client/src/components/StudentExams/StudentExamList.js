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
  return <Card>{exams}</Card>;
};

export default StudentExamList;
