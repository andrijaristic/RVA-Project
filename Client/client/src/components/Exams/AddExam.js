import React, { useState, useContext, useEffect } from "react";
import useHttp from "../../hooks/use-http";
import { useHistory } from "react-router-dom";

import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import AddExamForm from "./AddExamForm";

const AddExam = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { token } = authCtx;

  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [subjects, setSubjects] = useState(null);

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
      if (data.hasError) {
        setInfoData({
          title: data.title,
          message: data.errorMessage,
        });

        return;
      }

      const formattedData = data.map((item) => ({
        id: item.id,
        name: item.subjectName,
      }));

      setSubjects(formattedData);
    };

    getSubjects();
  }, [token, sendRequest]);

  useEffect(() => {
    console.log(subjects);
  }, [subjects]);

  const addExamHandler = async (examData) => {
    const subjectId = subjects.reduce(
      (prevValue, currentValue) =>
        currentValue.name === examData.subjectName
          ? currentValue.id
          : prevValue,
      0
    );

    console.log(subjectId);

    const requestConfig = {
      url: "https://localhost:44344/api/Exams",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        examName: examData.name,
        subjectId: subjectId,
        examDate: examData.date,
      }),
    };

    console.log(examData.date);
    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setInfoData({
        title: data.title,
        message: data.message,
      });
    }

    setInfoData({
      title: "Success",
      message: `Successfully added new exam for ${examData.subjectName}!`,
    });
  };

  const hideErrorModalHandler = () => {
    setInfoData(null);
  };

  const hideSuccessModalHandler = () => {
    setInfoData(null);
    history.replace("/");
  };

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {infoData && (
        <InfoModal
          title={infoData.title}
          message={infoData.message}
          onConfirm={
            infoData.message === "Error"
              ? hideErrorModalHandler
              : hideSuccessModalHandler
          }
        />
      )}
      {subjects !== null && (
        <AddExamForm
          title="New Exam creation"
          items={subjects}
          onSubmit={addExamHandler}
        />
      )}
    </React.Fragment>
  );
};

export default AddExam;
