import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import * as blogActions from "../../redux/modules/blogs";

class Info extends Component {
  componentDidMount() {
    console.log("xxx");
    console.log(this.props);
    this.props.getBlogInfo(1);
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

export default connect(mapStateToProps, mapDispatchToProps)(Info);
