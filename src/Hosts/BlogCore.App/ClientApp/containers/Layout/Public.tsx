import * as React from 'react';
import { Switch, Route, RouteComponentProps } from 'react-router-dom';
import { Container, Row, Col, Card, CardBody } from 'reactstrap';

import { PublicBlog, Counter, FetchData } from './../';

export default class Full extends React.Component<
  RouteComponentProps<{}>,
  {}
> {
  public render(): JSX.Element {
    const { match } = this.props;
    const routes: JSX.Element = (
      <Switch>
        <Route exact path={`${match.url}:username`} component={PublicBlog} />
        <Route path={`${match.url}public/counter`} component={Counter} />
        <Route
          path={`${match.url}public/fetchdata/:startDateIndex?`}
          component={FetchData}
        />
      </Switch>
    );
    return (
      <div className="app">
        <div className="app-body">
          <main className="main">
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
        </div>
      </div>
    );
  }
}
