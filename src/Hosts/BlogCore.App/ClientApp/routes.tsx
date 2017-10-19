import * as React from "react";
import { Route } from "react-router-dom";
import { Layout } from "./components/Layout";
import FetchData from "./components/FetchData";
import Counter from "./components/Counter";
import { FullLayout } from "./components/Layout/Full";
import { Dashboard } from "./containers/Dashboard";

export const routes = (
  <FullLayout>
    <Route exact path="/" component={Dashboard} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
  </FullLayout>
);
