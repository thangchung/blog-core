import React from "react";
import { IndexRoute, Route } from "react-router";
import BlogLayout from "./containers/App/BlogLayout";
import SimpleBlogLayout from "./containers/App/SimpleBlogLayout";
import Home from "./containers/Home/Home";
import Setting from "./containers/Setting/Setting";
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
      <Route path="/">
        <Route name="SimpleBlogLayout" component={SimpleBlogLayout}>
          <Route name="Login" path="login" component={Login} />
          <Route path="callback" component={Callback} />
          <Route path="setting" component={Setting} onEnter={requireLogin} />
          <Route path="*" component={NotFound} status={404} />
        </Route>
        <Route name="BlogLayout" component={BlogLayout}>
          <IndexRoute component={Home} />
          <Route path=":id" component={Home} />
        </Route>
      </Route>
    </Route>
  );
};
