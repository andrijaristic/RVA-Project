import React, { useContext } from "react";
import { Route, Switch, Redirect } from "react-router-dom";

import AuthContext from "./store/auth-context";
import Header from "./components/Header/Header";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ProfilePage from "./pages/ProfilePage";

function App() {
  const authCtx = useContext(AuthContext);

  return (
    <React.Fragment>
      <Header />
      <Switch>
        <Route path="/login">
          {!authCtx.isLoggedIn && <LoginPage />}
          {authCtx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/register">
          {!authCtx.isLoggedIn && <RegisterPage />}
          {authCtx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/edit-profile">
          {authCtx.isLoggedIn && <ProfilePage />}
          {!authCtx.isLoggedIn && <Redirect to="/" />}
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
