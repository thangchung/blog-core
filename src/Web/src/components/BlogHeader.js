import React, { Component } from "react";

export default class BlogHeader extends Component {
  render() {
    if (!this.props.blog) {
      return <div>&nbsp;</div>;
    }
    return (
      <div className="blog-header">
        <div className="container">
          <h1 className="blog-title">{this.props.blog.title}</h1>
          <p className="lead blog-description">
            {this.props.blog.description}
          </p>
        </div>
      </div>
    );
  }
}
