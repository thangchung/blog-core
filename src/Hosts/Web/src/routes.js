// core libs
import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

// layout
import BlogLayout from "./containers/App/BlogLayout";

// TODO: work on it later
export default store => {
  const authenticate = (nextState, replace, cb) => {
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
    <Switch>
      <Route exact path="/" name="index" component={BlogLayout} />
    </Switch>
  );
};
