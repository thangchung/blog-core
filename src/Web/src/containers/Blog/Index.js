// libs
import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

// components
import * as postActions from "../../redux/modules/posts";
import BlogHeader from "../../components/BlogHeader.js";
import PostList from "../../components/PostList";

class Blog extends Component {
  componentDidMount() {
    this.props.getPosts(
      this.props.match.params.blogId,
      this.props.postStore.page
    );
  }

  handleOlderClick() {
    this.props.getPosts(
      this.props.match.params.blogId,
      this.props.postStore.page + 1
    );
  }

  handleNewerClick() {
    var page = this.props.postStore.page;
    this.props.getPosts(
      this.props.match.params.blogId,
      page > 1 ? page - 1 : 1
    );
  }

  render() {
    const { postStore: { loading, bySlugs, posts, blog } } = this.props;
    if (loading) {
      return <div>Loading...</div>;
    }

    if (bySlugs.length <= 0) {
      return (
        <div>
          <BlogHeader blog={blog} />
          <p>No post.</p>
        </div>
      );
    }

    return (
      <div>
        <BlogHeader blog={blog} />
        <PostList blogId={this.props.match.params.blogId} slugs={bySlugs} posts={posts} />
        {bySlugs.length > 0 &&
          <nav className="blog-pagination">
            <a
              onClick={() => this.handleOlderClick()}
              className="btn btn-outline-primary"
              href="#"
            >
              Older
            </a>&nbsp;
            <a
              onClick={() => this.handleNewerClick()}
              className="btn btn-outline-secondary"
              href="#"
            >
              Newer
            </a>
          </nav>}
      </div>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    userStore: state.oidc.user,
    postStore: state.postStore
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(postActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Blog);
