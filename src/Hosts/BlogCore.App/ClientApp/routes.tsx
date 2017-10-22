import * as React from "react";
import { Route, Switch } from "react-router-dom";

import { PublicLayout, FullLayout } from "./containers";

export const routeNames: any = {
  "/admin": "Dashboard",
  "/admin/blogs": "Blogs",
  "/admin/new-blog": "New blog"
};

export const routes: any = (
  <Switch>
    <Route path="/admin" component={FullLayout} />
    <Route path="/" component={PublicLayout} />
  </Switch>
);
