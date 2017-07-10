import React, { Component } from "react";
import { connect } from "react-redux";

class BlogHeader extends Component {
  render() {
    return (
      <div className="blog-header">
        <div className="container">
          <h1 className="blog-title">The Bootstrap Blog</h1>
          <p className="lead blog-description">
            An example blog template built with Bootstrap.
          </p>
        </div>
      </div>
    );
  }
}

export default connect(null, null)(BlogHeader);
