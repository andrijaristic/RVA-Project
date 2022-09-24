import React, { useContext, useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import classes from "./Logs.module.css";
import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import LogsList from "./LogsList";

const Logs = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);

  const { token } = authCtx;
  const { isLoading, sendRequest } = useHttp();

  const [logs, setLogs] = useState(null);
  const [errorData, setErrorData] = useState(null);

  useEffect(() => {
    const getLogs = async () => {
      const requestConfig = {
        url: "https://localhost:44344/api/Users/logs",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };

      const data = await sendRequest(requestConfig);
      if (data.hasError) {
        setErrorData({
          title: data.title,
          message: data.message,
        });
      }

      setLogs(data);
    };

    getLogs();
  }, [token, sendRequest]);

  const hideModalHandler = () => {
    setErrorData(null);
    history.replace("/");
  };

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {errorData && (
        <InfoModal
          title={errorData.title}
          message={errorData.message}
          onConfirm={hideModalHandler}
        />
      )}
      {logs !== null && <LogsList items={logs} />}
      {logs !== null && logs.length === 0 && (
        <section className={classes["no-logs"]}>
          <h2>There are no available logs!</h2>
        </section>
      )}
    </React.Fragment>
  );
};

export default Logs;
