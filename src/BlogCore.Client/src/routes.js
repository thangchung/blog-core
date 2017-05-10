import React from "react";
import { IndexRoute, Route } from "react-router";
import AppLayout from "./containers/App/AppLayout";
import Home from "./containers/Home/Home";
import BlogInfo from "./containers/Blog/Info";
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
    <Route path="/" component={AppLayout}>
      <IndexRoute component={Home} />
      <Route onEnter={requireLogin}>
        <Route path="blog-info" component={BlogInfo} />
      </Route>
      <Route path="login" component={Login} />
      <Route path="callback" component={Callback} />
      <Route path="*" component={NotFound} status={404} />
    </Route>
  );
};
