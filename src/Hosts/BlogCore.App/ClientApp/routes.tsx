import * as React from "react";
import { Route, Switch } from "react-router-dom";

import { PublicLayout, FullLayout } from "./containers";
import {
  PublicBlog,
  Dashboard,
  Counter,
  FetchData,
  BlogList,
  AddNewBlog
} from "./containers";

export const routeNames: any = {
  "/": "Home",
  "/blogs": "Blogs",
  "/new-blog": "New blog",
  "/counter": "Counter",
  "/fetchdata": "Fetch data"
};

export const routes: any = (
  <Switch>
    <Route path="/admin" component={FullLayout} />
    <Route path="/" component={PublicLayout} />
  </Switch>
);
