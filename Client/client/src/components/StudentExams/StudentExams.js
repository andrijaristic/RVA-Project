import React, { useState, useEffect, useCallback, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import StudentExamList from "./StudentExamList";

let isInit = true;

const StudentExams = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { token } = authCtx;
  const { isLoading, sendRequest } = useHttp();

  const params = useParams();
  const { studentId } = params;

  const [exams, setExams] = useState(null);
  const [infoData, setInfoData] = useState(null);

  useEffect(() => {
    isInit = true;
  }, []);

  const getExams = useCallback(async () => {
    const requestConfig = {
      url: `https://localhost:44344/api/StudentResults/get-exams/${studentId}`,
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

      return;
    }

    isInit = false;

    const options = { day: "numeric", month: "long", year: "numeric" };
    const formattedExams = data.map((item) => ({
      id: item.exam.id,
      name: item.exam.examName,
      subject: item.exam.subject.subjectName,
      date: new Date(item.exam.examDate).toLocaleDateString("en-US", options),
    }));

    setExams(formattedExams);
  }, [studentId, token, sendRequest]);

  useEffect(() => {
    if (isInit) {
      getExams();
    }

    const timer = setTimeout(async () => {
      await getExams();
    }, 10000);

    return () => {
      clearTimeout(timer);
    };
  }, [getExams]);

  const hideModalHandler = () => {
    setInfoData(null);
    history.replace("/students");
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
        studentId: studentId,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }
  };

  const editExamHandler = async (updatedExam) => {
    const requestConfig = {
      url: `https://localhost:44344/api/Exams/update-exam`,
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        id: updatedExam.id,
        examName: updatedExam.name,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }
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
      {exams !== null && exams.length === 0 && (
        <section>
          <h2>The student is not registered to any exams.</h2>
        </section>
      )}
      {exams !== null && (
        <StudentExamList
          items={exams}
          onRemove={removeStudentFromExamHandler}
          onEdit={editExamHandler}
        />
      )}
    </React.Fragment>
  );
};

export default StudentExams;
