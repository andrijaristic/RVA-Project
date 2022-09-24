import React from "react";
import ReactDOM from "react-dom";

import Card from "../Card/Card";
import Button from "../Button/Button";
import classes from "./InfoModal.module.css";

const Backdrop = (props) => {
  return <div className={classes.backdrop} onClick={props.onClick} />;
};

const ModalOverlay = (props) => {
  return (
    <Card className={classes.modal}>
      <header className={classes.header}>
        <h2>{props.title}</h2>
      </header>
      <div className={classes.content}>
        <p>{props.message}</p>
      </div>
      <footer className={classes.actions}>
        {!props.onClose && (
          <Button onClick={props.onConfirm} className={classes.button}>
            Ok
          </Button>
        )}
        {props.onClose && (
          <Button onClick={props.onClose} className={classes.button}>
            Close
          </Button>
        )}
      </footer>
    </Card>
  );
};

const InfoModal = (props) => {
  return (
    <React.Fragment>
      {ReactDOM.createPortal(
        <Backdrop onClick={props.onClose ? props.onClose : props.onConfirm} />,
        document.getElementById("backdrop-root")
      )}
      {ReactDOM.createPortal(
        <ModalOverlay
          title={props.title}
          message={props.message}
          onConfirm={props.onConfirm}
          onClose={props.onClose}
        />,
        document.getElementById("overlay-root")
      )}
    </React.Fragment>
  );
};

export default InfoModal;
