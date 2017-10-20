import * as React from "react";
import { Route } from "react-router-dom";
import { FullLayout } from "./components";
import { Dashboard, Counter, FetchData, BlogList } from "./containers";

export const routes: any = (
  <FullLayout>
    <Route exact path="/" component={Dashboard} />
    <Route path="/blogs/:page?" component={BlogList} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
  </FullLayout>
);
