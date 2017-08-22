// libs
import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

// components
import * as postActions from "../../redux/modules/posts";

class PostDetail extends Component {
  componentDidMount() {
    this.props.loadBlogPostById(this.props.match.params.blogId, this.props.match.params.postId);
  }

  render() {
    if (this.props.postStore.loading) {
      return <div>Loading...</div>;
    }

    const { post } = this.props.postStore;
    return (
      <div key={post.id} className="blog-post">
        <h2 className="blog-post-title">
          {post.title}<span className="small">- {post.excerpt}</span>
        </h2>
        <p>{post.body}</p>
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

export default connect(mapStateToProps, mapDispatchToProps)(PostDetail);
