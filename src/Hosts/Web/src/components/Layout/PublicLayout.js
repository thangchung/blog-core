import React, { Component } from "react";
import { Switch, Route } from "react-router-dom";

// our components
import Header from "../../components/Header";
import Footer from "../../components/Footer";
import About from "../../components/About";

// our containers
import HomeContainer from "../../containers/Home/Index";
import BlogContainer from "../../containers/Blog/Index";
import PostDetailContainer from "../../containers/Post/PostDetail";

// style
import "./blog.css";

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
                  component={HomeContainer}
                />
                <Route
                  exact
                  path={`${match.url}blogs/:blogId`}
                  key="blogId"
                  component={BlogContainer}
                />
                <Route
                  exact
                  path={`${match.url}blogs/:blogId/posts/:postId`}
                  key="postDetail"
                  component={PostDetailContainer}
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
