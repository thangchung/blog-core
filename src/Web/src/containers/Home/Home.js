import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import * as blogActions from "../../redux/modules/blogs";
import * as postActions from "../../redux/modules/posts";

class Home extends Component {
  componentDidMount() {
    this.props.getBlogsByPage(this.props.blogStore.page);
  }

  handleRedirectToBlog(blog)
  {
    this.props.redirectToSpecificBlog(blog);
    this.props.history.push(`${this.props.match.url}blog/${blog.id}`);
  }

  render() {
    const { match, blogStore: { loading, byIds, blogs } } = this.props;
    return (
      <ul>
        {byIds.map((id, index) =>
          <li key={index}>
            <a href="#" onClick={()=>this.handleRedirectToBlog(blogs[id])}>{blogs[id].title}</a>
          </li>
        )}
      </ul>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    userStore: state.oidc.user,
    blogStore: state.blogStore
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators({...blogActions, ...postActions}, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
