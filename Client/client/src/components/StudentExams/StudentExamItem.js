import Card from "../UI/Card/Card";

const StudentExamItem = (props) => {
  return (
    <Card>
      <section>
        <h2>{props.name}</h2>
      </section>
      <div>
        <figure>
          <label htmlFor="date">Exam date</label>
          <p id="date">{props.date}</p>
        </figure>
      </div>
    </Card>
  );
};

export default StudentExamItem;
