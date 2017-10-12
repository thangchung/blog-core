// libs
import React, { Component } from "react";
import { Switch, Route } from "react-router-dom";

// our components
import Header from "../../components/Header";
import Footer from "../../components/Footer";

// our containers
import Restricted from "../Restricted";
import DashboardContainer from "../../containers/Dashboard/Index";
import PostManagementContainer from "../../containers/Post/PostManagement";
import ProfileSettingContainer from "../../containers/Setting/ProfileSetting";

export default class AdminLayout extends Component {
  render() {
    const { match } = this.props;
    return (
      <div className="app">
        <Header />
        <div className="container">
          <div className="row">
            <div className="col-sm-12 blog-main">
              <Switch>
                <Route
                  exact
                  path={`${match.url}/dashboard`}
                  key="dashboard"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(DashboardContainer)}
                    </Restricted>
                  )}
                />
                <Route
                  exact
                  path={`${match.url}/posts`}
                  key="post-management"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(PostManagementContainer)}
                    </Restricted>
                  )}
                />
                <Route
                  exact
                  path={`${match.url}/profile-setting`}
                  key="profile-setting"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(ProfileSettingContainer)}
                    </Restricted>
                  )}
                />
              </Switch>
            </div>
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}
