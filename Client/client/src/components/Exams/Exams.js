import React, { useContext, useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import AuthContext from "../../store/auth-context";
import ExamList from "./ExamList";

let isInit = true;

const Exams = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { user, token } = authCtx;

  const isAdmin = user.userType === 0;

  const { isLoading, sendRequest } = useHttp();

  const [exams, setExams] = useState(null);
  const [errorData, setErrorData] = useState(null);

  useEffect(() => {
    const getExams = async () => {
      const requestConfig = {
        url: "https://localhost:44344/api/Exams",
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

      setExams(data);
    };

    if (isInit) {
      getExams();
    }

    const timer = setTimeout(async () => {
      await getExams();
    }, 10000);

    return () => {
      clearTimeout(timer);
    };
  }, [token, sendRequest]);

  // Radi LoadingModal na prvom otvaranju/rucnom refresh stranice.
  useEffect(() => {
    isInit = true;
  }, []);

  const hideModalHandler = (props) => {
    setErrorData(null);
    history.replace("/");
  };

  const addStudentToExamHandler = async (examId) => {
    const requestConfig = {
      url: "https://localhost:44344/api/StudentResults/apply",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        examId: examId,
        studentId: user.id,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setErrorData({
        title: data.title,
        message: data.message,
      });
    }
  };

  const removeStudentFromExamHandler = async (examId) => {
    console.log("You are now removing yourself from the exam.");
  };

  const viewExamResultHandler = async (examId) => {
    console.log(`You are now viewing EXAM:${examId} results.`);
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
      {exams && (
        <ExamList
          items={exams}
          admin={isAdmin}
          onAdd={addStudentToExamHandler}
          onRemove={removeStudentFromExamHandler}
          onView={viewExamResultHandler}
        />
      )}
      {exams && exams.length === 0 && (
        <section>
          <h1>No exams available!</h1>
        </section>
      )}
    </React.Fragment>
  );
};

export default Exams;
