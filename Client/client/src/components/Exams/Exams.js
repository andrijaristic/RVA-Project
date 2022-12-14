import React, { useContext, useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import classes from "./Exams.module.css";
import Card from "../UI/Card/Card";
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
  const [infoData, setInfoData] = useState(null);
  const [toggle, setToggle] = useState(false);

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
        setInfoData({
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
      setToggle((prevState) => !prevState);
    }, 12000);

    return () => {
      clearTimeout(timer);
    };
  }, [token, sendRequest, toggle]);

  // Radi LoadingModal na prvom otvaranju/rucnom refresh stranice.
  useEffect(() => {
    isInit = true;
  }, []);

  const hideModalHandler = (props) => {
    setInfoData(null);
    history.replace("/exams");
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
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }

    authCtx.onApplication({ id: data.id, date: data.examDate });
  };

  const removeStudentFromExamHandler = async (examId, examDate) => {
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
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }

    authCtx.onWithdrawal({ id: examId, date: examDate });
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
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }

    console.log(`Exam result: ${data.result ? "Passed." : "Failed."}`);
    setInfoData({
      title: "EXAM RESULTS",
      message: data.result ? "Passed." : "Failed.",
    });
  };

  return (
    <React.Fragment>
      {isLoading && isInit && <LoadingModal />}
      {infoData && (
        <InfoModal
          title={infoData.title}
          message={infoData.message}
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
        <Card className={classes.nothing}>
          <section className={classes["no-exams"]}>
            <h2>No exams available!</h2>
          </section>
        </Card>
      )}
    </React.Fragment>
  );
};

export default Exams;
