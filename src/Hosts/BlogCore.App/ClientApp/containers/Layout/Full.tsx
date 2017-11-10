import * as React from 'react';
import { bindActionCreators, Dispatch } from 'redux';
import { connect } from 'react-redux';
import { RouteComponentProps, Switch, Route } from 'react-router-dom';
import { Container, Row, Col, Card, CardBody } from 'reactstrap';

import { Breadcrumb, Sidebar, Header, Footer } from 'OurComponents';
import { Dashboard, BlogManagement, BlogForm } from './../';
import { ApplicationState } from 'OurModules';

interface FullAppProps {
  onRedirect: () => any;
}

type ExFullAppProps = ApplicationState &
  FullAppProps &
  RouteComponentProps<any>;

class Full extends React.Component<ExFullAppProps, any> {
  componentWillReceiveProps(nextProps: any) {
    if (nextProps.common.to) {
      console.log(nextProps);
      this.props.history.replace(nextProps.common.to);
      this.props.onRedirect();
    }
  }

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

function mapDispatchToProps(dispatch: Dispatch<any>): FullAppProps {
  return bindActionCreators(
    {
      onRedirect: (): any => {
        return dispatch({ type: 'REDIRECT_TO', to: null });
      }
    },
    dispatch
  );
}

export default connect((state: ApplicationState) => state, mapDispatchToProps)(
  Full
);
