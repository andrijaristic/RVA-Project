import classes from "./LogItem.module.css";

const LogItem = (props) => {
  return (
    <li className={classes.log}>
      <div>
        <div className={classes.errorType}>
          <p
            className={`${
              props.eventType === "ERR" ? classes.err : classes.inf
            }`}
          >
            {`[${props.eventType}]`}
          </p>
          &nbsp;
          <p className={classes.timestamp}>{props.timestamp}</p>
        </div>
        {props.message}
      </div>
    </li>
  );
};

export default LogItem;
