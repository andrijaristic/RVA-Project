import React from "react";
import ReactDOM from "react-dom";
import { useHistory } from "react-router-dom";

import Card from "../Card/Card";
import classes from "./LoadingModal.module.css";

const Backdrop = (props) => {
  return <div className={classes.backdrop} onClick={props.onClick} />;
};

const ModalOverlay = (props) => {
  return (
    <Card className={classes.modal}>
      <div className={classes.spinner}></div>
    </Card>
  );
};
const LoadingModal = (props) => {
  const history = useHistory();

  const clickHandler = () => {
    history.push("/");
  };

  return (
    <React.Fragment>
      {ReactDOM.createPortal(
        <Backdrop onClick={clickHandler} />,
        document.getElementById("backdrop-root")
      )}
      {ReactDOM.createPortal(
        <ModalOverlay />,
        document.getElementById("overlay-root")
      )}
    </React.Fragment>
  );
};

export default LoadingModal;
