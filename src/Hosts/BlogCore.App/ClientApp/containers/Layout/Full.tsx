import * as React from "react";
import { Switch, Route } from "react-router-dom";
import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  CardFooter,
  CardBody
} from "reactstrap";

import { Aside, Breadcrumb, Sidebar, Header, Footer } from "Components";

import { Dashboard, BlogManagement, BlogForm } from "./../";

export default class Full extends React.Component<any, any> {
  public render(): JSX.Element {
    const { match } = this.props;
    const routes: JSX.Element = (
      <Switch>
        <Route exact path={`${match.url}`} component={Dashboard} />
        <Route path={`${match.url}/blogs/:page?`} component={BlogManagement} />
        <Route path={`${match.url}/blog/:id?`} component={BlogForm} />
      </Switch>
    );

    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props} />
          <main className="main">
            <Breadcrumb />
            <Container fluid>
              <Row>
                <Col xs="12" sm="12" md="12">
                  <Card>
                    <CardBody className="card-body">{routes}</CardBody>
                  </Card>
                </Col>
              </Row>
            </Container>
          </main>
          {/*<Aside /> */}
        </div>
        <Footer />
      </div>
    );
  }
}
