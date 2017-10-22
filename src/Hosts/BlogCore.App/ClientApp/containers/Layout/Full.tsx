import * as React from "react";
import { Switch, Route } from "react-router-dom";
import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  CardFooter,
  CardBlock
} from "reactstrap";

import { Aside, Breadcrumb, Sidebar, Header, Footer } from "./../../components";

import { Dashboard, BlogList, AddNewBlog } from "./../";

export default class Full extends React.Component<any, any> {
  public render(): JSX.Element {
    const { match } = this.props;
    const routes: JSX.Element = (
      <Switch>
        <Route exact path={`${match.url}`} component={Dashboard} />
        <Route path={`${match.url}/blogs/:page?`} component={BlogList} />
        <Route path={`${match.url}/new-blog`} component={AddNewBlog} />
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
                    <CardBlock className="card-body">{routes}</CardBlock>
                  </Card>
                </Col>
              </Row>
            </Container>
          </main>
          <Aside />
        </div>
        <Footer />
      </div>
    );
  }
}
