import React, { useState, useContext, useEffect } from "react";
import { useHistory } from "react-router-dom";
import useHttp from "../../hooks/use-http";

import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import AuthContext from "../../store/auth-context";
import StudentsList from "./StudentsList";
import StudentsFilter from "./StudentsFilter";

let isInit = true;

const filterStudents = (students, filters) => {
  return students.filter((item) => {
    const nameFilter = item.name.toLowerCase().includes(filters.name);
    const lastNameFilter = item.lastName
      .toLowerCase()
      .includes(filters.lastName);
    const examFilter =
      filters.exam === "" ||
      item.exams.filter((exam) =>
        exam.examName.toLowerCase().includes(filters.exam)
      ).length !== 0;

    return nameFilter && lastNameFilter && examFilter;
  });
};

const Students = () => {
  const history = useHistory();
  const authCtx = useContext(AuthContext);
  const { token } = authCtx;

  const { isLoading, sendRequest } = useHttp();
  const [students, setStudents] = useState(null);
  const [exams, setExams] = useState(null);

  const initFilter = {
    name: "",
    lastName: "",
    exam: "",
  };

  const [filter, setFilter] = useState(initFilter);
  const [infoData, setInfoData] = useState(null);

  useEffect(() => {
    const getStudents = async () => {
      const requestConfig = {
        url: "https://localhost:44344/api/Students",
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

      setStudents(data);
      console.log(data);
    };

    if (isInit) {
      getStudents();
    }

    const timer = setTimeout(async () => {
      await getStudents();
    }, 10000);

    return () => {
      clearTimeout(timer);
    };
  }, [token, sendRequest]);

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
      console.log(data);
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

  useEffect(() => {
    isInit = true;
  }, []);

  const hideModalHandler = (props) => {
    setInfoData(null);
    history.replace("/");
  };

  const applyFilterHandler = (data) => {
    setFilter(data);
  };

  const removeFilterHandler = (data) => {
    setFilter(initFilter);
  };

  let filteredStudents;
  if (students !== null) {
    filteredStudents = filterStudents(students, filter);
    console.log(filteredStudents);
  }

  let examsFilter;
  if (exams !== null) {
    examsFilter = exams.map((item) => ({
      id: item.id,
      name: item.examName,
      date: item.examDate,
    }));
  }

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
      {students !== null && students.length !== 0 && (
        <StudentsFilter
          items={examsFilter}
          onApply={applyFilterHandler}
          onRemove={removeFilterHandler}
        />
      )}
      {students !== null && (
        <StudentsList
          items={filteredStudents}
          onClick={() => {
            console.log("hello");
          }}
        />
      )}
      {students && students.length === 0 && (
        <section>
          <h1>There are no registered students!</h1>
        </section>
      )}
    </React.Fragment>
  );
};

export default Students;
