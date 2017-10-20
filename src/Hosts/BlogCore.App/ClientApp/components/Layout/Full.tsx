import * as React from "react";
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
              <Row>
                <Col xs="12" sm="12" md="12">
                  <Card>
                    <CardBlock className="card-body">
                      <div>{this.props.children}</div>
                    </CardBlock>
                  </Card>
                </Col>
              </Row>
            </Container>
          </main>
        </div>
      </div>
    );
  }
}
