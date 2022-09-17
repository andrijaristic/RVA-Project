import React, { useRef, useState, useContext } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import AddSubjectForm from "./AddSubjectForm";

const AddSubject = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { token } = authCtx;

  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [errorData, setErrorData] = useState(null);

  const addSubjectHandler = async (subjectData) => {
    const requestConfig = {
      url: "https://localhost:44344/api/Subjects",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        subjectName: subjectData.subjectName,
      }),
    };

    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setErrorData({
        title: data.title,
        message: data.errorMessage,
      });
    }

    setInfoData({
      title: data.title,
      message: data.message,
    });

    history.replace("/subjects");
  };

  const hideErrorModalHandler = () => {
    setInfoData(null);
  };

  const hideSuccessModalHandler = () => {
    setInfoData(null);
    history.replace("/subjects");
  };

  return (
    <React.Fragment>
      {/* <InfoModal
        title={errorData ? errorData.title : infoData.title}
        message={errorData ? errorData.message : infoData.message}
        onConfirm={
          errorData.hasError ? hideErrorModalHandler : hideSuccessModalHandler
        }
      /> */}
      <AddSubjectForm title="New Subject" onSubmit={addSubjectHandler} />
    </React.Fragment>
  );
};

export default AddSubject;
