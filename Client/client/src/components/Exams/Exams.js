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
          Authorization: `Bearer ${token}`,
        },
      };

      const data = await sendRequest(requestConfig);
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
        studentId: user.studentId,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setErrorData({
        title: data.title,
        message: data.message,
      });
    }

    authCtx.onApplication({ id: data.id, date: data.examDate });
  };

  const removeStudentFromExamHandler = async (examId) => {
    const requestConfig = {
      url: "https://localhost:44344/api/StudentResults/remove",
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        examId: examId,
        studentId: user.studentId,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setErrorData({
        title: data.title,
        message: data.message,
      });
    }

    authCtx.onWithdrawal({ id: data.id, date: data.examDate });
  };

  const viewExamResultHandler = async (examId) => {
    const requestConfig = {
      url: `https://localhost:44344/api/StudentResults/view?ExamId=${examId}&StudentId=${user.studentId}`,
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

    console.log(`Exam result: ${data.result ? "Passed." : "Failed."}`);
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
