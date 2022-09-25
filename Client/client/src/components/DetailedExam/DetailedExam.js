import React, { useState, useEffect, useCallback, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import DetailedExamList from "./DetailedExamList";

let isInit = true;

const DetailedExam = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { token } = authCtx;
  const { isLoading, sendRequest } = useHttp();

  const params = useParams();
  const { examId } = params;

  const [students, setStudents] = useState(null);
  const [infoData, setInfoData] = useState(null);
  const [toggle, setToggle] = useState(false);

  useEffect(() => {
    isInit = true;
  }, []);

  const getStudents = useCallback(async () => {
    const requestConfig = {
      url: `https://localhost:44344/api/StudentResults/get-students/${examId}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setInfoData({
        title: data.title,
        message: data.message,
        isError: data.hasError,
      });

      return;
    }

    isInit = false;
    setStudents(data);
  }, [examId, token, sendRequest]);

  useEffect(() => {
    if (isInit) {
      getStudents();
    }

    const timer = setTimeout(async () => {
      await getStudents();
      setToggle((prevState) => !prevState);
    }, 12000);

    return () => {
      clearTimeout(timer);
    };
  }, [getStudents, toggle]);

  const hideModalHandler = () => {
    setInfoData(null);
    history.replace("/exams");
  };

  const gradingHandler = async (studentId, didPass) => {
    const requestConfig = {
      url: "https://localhost:44344/api/StudentResults/",
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        studentId: studentId,
        examId: examId,
        result: didPass,
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
          onClose={hideModalHandler}
        />
      )}
      {students !== null && (
        <DetailedExamList
          id={examId}
          items={students}
          label="Result:"
          onClick={gradingHandler}
        />
      )}
    </React.Fragment>
  );
};

export default DetailedExam;
