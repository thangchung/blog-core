import React from "react";
import { IndexRoute, Route } from "react-router";
import SimpleLayout from "./containers/App/SimpleLayout";
import FullLayout from "./containers/App/FullLayout";
import Home from "./containers/Home/Home";
import BlogInfo from "./containers/Blog/BlogInfo";
import Login from "./containers/Login/Login";
import Callback from "./containers/Login/Callback";
import NotFound from "./containers/NotFound/NotFound";

export default store => {
  const requireLogin = (nextState, replace, cb) => {
    function checkAuth() {
      const { oidc: { user } } = store.getState();
      if (!user) {
        // oops, not logged in, so can't be here!
        replace("/");
      }
      cb();
    }
    checkAuth();
  };

  return (
    <Route>
      <Route path="/" name="Dashboard" component={SimpleLayout}>
        <IndexRoute component={Home} />
        <Route name="Login" path="login" component={Login} />
        <Route path="callback" component={Callback} />
      </Route>

      <Route name="Admin" path="admin" onEnter={requireLogin} component={FullLayout}>
        <IndexRoute component={Home} />
        <Route name="Blog Info" path="blog-info" component={BlogInfo} />
      </Route>
      <Route path="*" component={NotFound} status={404} />
    </Route>
  );
};
