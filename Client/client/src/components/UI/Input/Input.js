import React from "react";

import classes from "./Input.module.css";

const Input = React.forwardRef((props, ref) => {
  const inputClasses = `${classes.control} ${
    !props.isValid && props.isTouched ? classes.invalid : ""
  }`;
  return (
    <div className={inputClasses}>
      <label htmlFor={props.id}>{props.label}</label>
      <input
        id={props.id}
        ref={ref}
        type={props.type}
        onChange={props.onChange}
        onBlur={props.onBlur}
        placeholder={props.name}
      />
      {!props.isValid && props.isTouched && (
        <p>
          Please enter a valid {props.label.toLowerCase()} (5 or more
          characters).
        </p>
      )}
    </div>
  );
});

export default Input;
