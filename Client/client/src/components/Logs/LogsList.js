import Card from "../UI/Card/Card";
import LogItem from "./LogItem";

const LogsList = (props) => {
  const logs = props.items.map((item) => {
    return (
      <LogItem
        key={Math.random()}
        timestmap={item.timestamp}
        eventType={item.eventType}
        message={item.message}
      />
    );
  });

  return (
    <section>
      <Card>
        <section>
          <h1>Logs</h1>
        </section>
        <ul>{logs}</ul>
      </Card>
    </section>
  );
};

export default LogsList;
