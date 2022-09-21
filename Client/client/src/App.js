import React, { useContext } from "react";
import { Route, Switch, Redirect } from "react-router-dom";

import AuthContext from "./store/auth-context";
import Header from "./components/Header/Header";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ProfilePage from "./pages/ProfilePage";
import SubjectsPage from "./pages/SubjectsPage";
import ExamsPage from "./pages/ExamsPage";
import AddSubjectPage from "./pages/AddSubjectPage";
import AddExamPage from "./pages/AddExamPage";
import DetailedExamPage from "./pages/DetailedExamPage";
import StudentsPage from "./pages/StudentsPage";
import LogsPage from "./pages/LogsPage";
import StudentExamsPage from "./pages/StudentExamsPage";

function App() {
  const authCtx = useContext(AuthContext);
  console.log(authCtx.user);
  const isAdmin =
    authCtx.user !== null &&
    authCtx.user !== undefined &&
    authCtx.user.userType === 0;

  console.log(isAdmin);
  console.log(authCtx.isLoggedIn);

  return (
    <React.Fragment>
      <Header />
      <Switch>
        <Route path="/login">
          {!authCtx.isLoggedIn && <LoginPage />}
          {authCtx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/logs">
          {authCtx.isLoggedIn && <LogsPage />}
          {!authCtx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/register">
          {isAdmin && <RegisterPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/students" exact>
          {isAdmin && <StudentsPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/students/:studentId" exact>
          {isAdmin && <StudentExamsPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/edit-profile">
          {authCtx.isLoggedIn && <ProfilePage />}
          {!authCtx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/subjects">
          <SubjectsPage />
        </Route>
        <Route path="/add-subject">
          {isAdmin && <AddSubjectPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/exams" exact>
          {authCtx.isLoggedIn && <ExamsPage />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/exams/:examId" exact>
          {isAdmin && <DetailedExamPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/add-exam">
          {isAdmin && <AddExamPage />}
          {authCtx.isLoggedIn && !isAdmin && <Redirect to="/" />}
          {!authCtx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/" exact>
          <HomePage />
        </Route>
        <Route path="*">
          <Redirect to="/" />
        </Route>
      </Switch>
    </React.Fragment>
  );
}

export default App;
