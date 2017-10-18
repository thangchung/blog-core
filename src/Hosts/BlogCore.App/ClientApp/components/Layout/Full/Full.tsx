import * as React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { Container } from "reactstrap";
import { Header } from "../../../components/Header";

export default class Full extends React.Component<any, any> {
  public render() {
    return (
      <div className="app">
        <Header />
      </div>
    );
  }
}
