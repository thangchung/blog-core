import React from "react";
import { IndexRoute, Route } from "react-router";
import AppLayout from "./containers/App/AppLayout";
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
    <Route path="/" name="Dashboard" component={FullLayout}>
      <IndexRoute component={Home} />
      <Route onEnter={requireLogin}>
        <Route name="Blog Info" path="blog-info" component={BlogInfo} />
      </Route>
      <Route name="Login" path="login" component={Login} />
      <Route path="callback" component={Callback} />
      <Route path="*" component={NotFound} status={404} />
    </Route>
  );
};
