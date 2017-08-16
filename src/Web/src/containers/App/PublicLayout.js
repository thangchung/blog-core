import React, { Component } from "react";
import { Switch, Route } from "react-router-dom";

// our components
import Header from "../../components/Header";
import Footer from "../../components/Footer";
import About from "../../components/About";

// our containers
import HomePage from "../Home/Index";
import BlogPage from "../Blog/Index";
import PostDetailPage from "../Post/PostDetail";

export default class PublicLayout extends Component {
  render() {
    const { match } = this.props;
    return (
      <div className="app">
        <Header />
        <div className="container">
          <div className="row">
            <div className="col-sm-8 blog-main">
              <Switch>
                <Route
                  exact
                  path={`${match.url}`}
                  key="index"
                  component={HomePage}
                />
                <Route
                  exact
                  path={`${match.url}blog/:blogId`}
                  key="blogId"
                  component={BlogPage}
                />
                <Route
                  exact
                  path={`${match.url}post/:postId`}
                  key="postDetail"
                  component={PostDetailPage}
                />
              </Switch>
            </div>
            <div className="col-sm-3 offset-sm-1 blog-sidebar">
              <div className="sidebar-module sidebar-module-inset">
                <About />
              </div>
            </div>
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}
