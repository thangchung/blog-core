import * as React from "react";
import { Route } from "react-router-dom";

import { FullLayout } from "./components";
import {
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
  <FullLayout>
    <Route exact path="/" component={Dashboard} />
    <Route path="/blogs/:page?" component={BlogList} />
    <Route path="/new-blog" component={AddNewBlog} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
  </FullLayout>
);
