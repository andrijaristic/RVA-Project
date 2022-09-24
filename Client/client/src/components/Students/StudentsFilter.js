import React, { useState } from "react";

import classes from "./StudentsFilter.module.css";
import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import StudentsFilterForm from "./StudentsFilterForm";

const StudentsFilter = (props) => {
  const [showFilter, setShowFilter] = useState(false);

  const showFilterHandler = () => {
    setShowFilter(true);
  };

  const hideFilterHandler = () => {
    setShowFilter(false);
    props.onRemove();
  };

  return (
    <Card className={classes.filter}>
      {!showFilter && <Button onClick={showFilterHandler}>Filter</Button>}
      {showFilter && (
        <StudentsFilterForm
          items={props.items}
          onRemove={hideFilterHandler}
          onApply={props.onApply}
        />
      )}
    </Card>
  );
};

export default StudentsFilter;
