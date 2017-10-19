import * as React from "react";
import { Container } from "reactstrap";
import { Header } from "./../../../components/Header";
import { Sidebar } from "./../../../components/Sidebar";
// import Breadcrumb from './../../../components/Breadcrumb';
// import Aside from '../../components/Aside/';
// import Footer from '../../components/Footer/';

export default class Full extends React.Component<any, any> {
  public render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props} />
          <main className="main">
            <Container fluid>
              <div>{this.props.children}</div>
            </Container>
          </main>
        </div>
      </div>
    );
  }
}
