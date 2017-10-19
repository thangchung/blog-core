import * as React from "react";
import { Container } from "reactstrap";
import { Aside, Breadcrumb, Sidebar, Header, Footer } from "./../../components";

export default class Full extends React.Component<any, any> {
  public render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props} />
          <main className="main">
            <Breadcrumb />
            <Container fluid>
              <div>{this.props.children}</div>
            </Container>
          </main>
        </div>
      </div>
    );
  }
}
