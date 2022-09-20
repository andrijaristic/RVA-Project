import Card from "../UI/Card/Card";
import DetailedExamItem from "./DetailedExamItem";

const DetailedExamList = (props) => {
  const students = props.items.map((item) => {
    return (
      <DetailedExamItem
        key={Math.random()}
        id={item.id}
        studentName={`${item.student.name} ${item.student.lastName}`}
        label={props.label}
        result={item.result ? "Passed" : "Failed"}
        onClick={props.onClick}
        touched={item.isTouched}
      ></DetailedExamItem>
    );
  });

  return (
    <Card>
      <ul>{students}</ul>
    </Card>
  );
};

export default DetailedExamList;
