import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import classes from "./Subjects.module.css";
import AuthContext from "../../store/auth-context";
import SubjectsList from "./SubjectsList";
import LoadingModal from "../UI/Modals/LoadingModal";
import InfoModal from "../UI/Modals/InfoModal";

let isInit = true;

const Subjects = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);

  const { token } = authCtx;
  const { isLoading, sendRequest } = useHttp();

  const [subjects, setSubjects] = useState(null);
  const [errorData, setErrorData] = useState(null);
  const [toggle, setToggle] = useState(false);

  useEffect(() => {
    isInit = true;
  }, []);

  useEffect(() => {
    const getSubjects = async () => {
      const requestConfig = {
        url: "https://localhost:44344/api/Subjects",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };

      const data = await sendRequest(requestConfig);

      // ErrorDTO and SuccessDTO logic. (missing on back-end)
      if (data.hasError) {
        setErrorData({
          title: data.title,
          message: data.errorMessage,
        });
      }

      setSubjects(data);
    };

    if (isInit) {
      getSubjects();
    }

    const timer = setTimeout(async () => {
      await getSubjects();
      setToggle((prevState) => !prevState);
    }, 12000);

    return () => {
      clearTimeout(timer);
    };
  }, [token, sendRequest, toggle]);

  const hideModalHandler = () => {
    setErrorData(null);
    history.replace("/");
  };

  return (
    <React.Fragment>
      {isLoading && isInit && <LoadingModal />}
      {errorData && (
        <InfoModal
          title={errorData.title}
          message={errorData.message}
          onConfirm={hideModalHandler}
        />
      )}
      {subjects && <SubjectsList items={subjects} />}
      {subjects && subjects.length === 0 && (
        <section className={classes["no-subjects"]}>
          <h2>No subjects available!</h2>
        </section>
      )}
    </React.Fragment>
  );
};

export default Subjects;
