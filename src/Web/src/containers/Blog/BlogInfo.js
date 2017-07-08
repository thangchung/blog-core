import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import * as blogActions from "../../redux/modules/blogs";

class BlogInfo extends Component {
  componentDidMount() {
    this.props.getBlogInfo("5B1FA7C2-F814-47F2-A2F3-03866F978C49");
  }

  render() {
    const { blog } = this.props;
    return (
      <div>
        {blog && <h1>{blog.title}</h1>}
        {blog && <h3>{blog.description}</h3>}
      </div>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    blog: state.blogs.blog
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(blogActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(BlogInfo);
