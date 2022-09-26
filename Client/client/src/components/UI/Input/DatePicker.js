import React from "react";

import classes from "./DatePicker.module.css";

const DatePicker = React.forwardRef((props, ref) => {
  const inputClasses = `${classes.control} ${
    !props.isValid && props.isTouched ? classes.invalid : ""
  }`;

  return (
    <div className={inputClasses}>
      <label htmlFor={props.id}>{props.label}</label>
      <input
        ref={ref}
        type="datetime-local"
        min={props.min}
        onChange={props.onChange}
        onBlur={props.onBlur}
      />
      {!props.isValid && props.isTouched && (
        <p>
          Please enter a valid {props.label.toLowerCase()}. (Field must not be
          empty!)
        </p>
      )}
    </div>
  );
});

export default DatePicker;
