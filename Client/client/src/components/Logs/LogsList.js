import classes from "./LogsList.module.css";
import Card from "../UI/Card/Card";
import LogItem from "./LogItem";

const LogsList = (props) => {
  const options = {
    day: "numeric",
    month: "long",
    year: "numeric",
    hours: "numeric",
    minutes: "numeric",
    seconds: "numeric",
  };
  const logs = props.items.map((item) => {
    const timestamp = new Date(item.timestamp).toLocaleTimeString(
      "en-US",
      options
    );
    return (
      <LogItem
        key={Math.random()}
        timestamp={timestamp}
        eventType={item.eventType}
        message={item.message}
      />
    );
  });

  return (
    <section className={classes.logs}>
      <Card className={classes["top-shorten"]}>
        <section className={classes.title}>
          <h1>Logs</h1>
        </section>
        <ul>{logs}</ul>
      </Card>
    </section>
  );
};

export default LogsList;
