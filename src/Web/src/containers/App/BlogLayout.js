import React, { Component } from "react";
import Header from "../../components/Header";
import Footer from "../../components/Footer";

export default class BlogLayout extends Component {
  render() {
    return (
      <div className="app">
        <Header />
        <div className="container">
          <div className="row">
            <div className="col-sm-8 blog-main">
              {this.props.children}
            </div>
            <div className="col-sm-3 offset-sm-1 blog-sidebar">
              <div className="sidebar-module sidebar-module-inset">
                <h4>About</h4>
                <p>
                  Etiam porta <em>sem malesuada magna</em> mollis euismod. Cras
                  mattis consectetur purus sit amet fermentum. Aenean lacinia
                  bibendum nulla sed consectetur.
                </p>
              </div>
            </div>
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}
