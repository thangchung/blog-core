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

  render() {
    const { match, blogStore: { loading, byIds, blogs } } = this.props;
    return (
      <ul>
        {byIds.map((id, index) =>
          <li key={index}>
            <a href={`${this.props.match.url}blog/${id}`}>
              {blogs[id].title}
            </a>
          </li>
        )}
      </ul>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    blogStore: state.blogStore
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators({ ...blogActions, ...postActions }, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
