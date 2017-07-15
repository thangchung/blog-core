import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import * as blogActions from "../../redux/modules/blogs";

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
            <Link to={`${match.url}blog/${id}`}>
              {blogs[id].title}
            </Link>
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
  bindActionCreators(blogActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
