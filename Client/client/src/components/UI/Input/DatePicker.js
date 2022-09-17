import React from "react";

import classes from "./DatePicker.module.css";

const DatePicker = React.forwardRef((props, ref) => {
  return (
    <div className={classes.control}>
      <label htmlFor={props.id}>{props.label}</label>
      <input ref={ref} type={props.type} min={props.minDate} />
    </div>
  );
});

export default DatePicker;
