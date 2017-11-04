import * as React from "react";
import { RouteComponentProps } from "react-router-dom";

export default class Dashboard extends React.Component<
  RouteComponentProps<{}>,
  any
> {
  render(): JSX.Element {
    return (
      <h1>Dashboard Page.</h1>  
    );
  }
}
