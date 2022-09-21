import React from "react";

const LogItem = (props) => {
  return (
    <li>
      <div>
        <h3>{props.timestamp}</h3>
        <div>
          <p>[{props.eventType}]</p>
          {props.message}
        </div>
      </div>
    </li>
  );
};

export default LogItem;
