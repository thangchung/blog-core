import React, { Component } from "react";
import Header from "../../components/Header";
import Footer from "../../components/Footer";

export default class SimpleBlogLayout extends Component {
  render() {
    return (
      <div className="app">
        <Header />
        <div className="container">
          <div className="row">
            <div className="col-sm-12 blog-main">
              {this.props.children}
            </div>
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}
