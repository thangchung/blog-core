import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import * as blogActions from "../../redux/modules/blogs";
import * as postActions from "../../redux/modules/posts";

class Home extends Component {
  componentDidMount() {
    this.props.getBlogsByPage(this.props.currentPage);
  }

  render() {
    const { match, blogByIds, blogs } = this.props;
    return (
      <ul>
        {blogByIds.map((id, index) => (
          <li key={index}>
            <a href={`${match.url}blogs/${id}`}>{blogs[id].title}</a>
          </li>
        ))}
      </ul>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    currentPage: state.blogStore.page,
    blogByIds: state.blogStore.byIds,
    blogs: state.blogStore.blogs
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators({ ...blogActions, ...postActions }, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
