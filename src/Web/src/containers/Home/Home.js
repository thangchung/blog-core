import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router";
import { Button } from "reactstrap";
import moment from "moment";
import userManager from "../../utils/userManager";
import * as postActions from "../../redux/modules/posts";
import BlogHeader from "../../components/BlogHeader.js";

export class PostList extends Component {
  render() {
    const { ids, posts } = this.props;

    return (
      <div>
        {ids.map((id, index) =>
          <div key={id} className="blog-post">
            <h2 className="blog-post-title">{posts[id].title}</h2>
            <p className="blog-post-meta">
              {moment(posts[id].created_at).format(
                "MMMM Do, YYYY"
              )}&nbsp;by&nbsp;
              <a href="#">
                {`${posts[id].author.givenName} ${posts[id].author.familyName}`}
              </a>
            </p>

            <p>{posts[id].excerpt}</p>
          </div>
        )}
      </div>
    );
  }
}

class Home extends Component {
  componentWillMount() {
    // TODO: redirect if login already based on the profile.blogId
  }

  componentDidMount() {
    console.log(this.props);
    this.props.getPosts(
      this.props.userStore.profile.blog_id,
      this.props.postStore.page
    );
  }

  handleOlderClick() {
    this.props.getPosts(
      this.props.userStore.profile.blog_id,
      this.props.postStore.page + 1
    );
  }

  handleNewerClick() {
    var page = this.props.postStore.page;
    this.props.getPosts(
      this.props.userStore.profile.blog_id,
      page > 1 ? page - 1 : 1
    );  
  }

  render() {
    const { loading, byIds, posts } = this.props.postStore;
    if (loading) {
      return <div>Loading...</div>;
    }
    return (
      <div>
        <BlogHeader />
        <PostList ids={byIds} posts={posts} />
        <nav className="blog-pagination">
          <a onClick={() => this.handleOlderClick()} href="#">
            Older
          </a>
          <a
            onClick={() => this.handleNewerClick()}
            className="btn btn-outline-secondary"
            href="#"
          >
            Newer
          </a>
        </nav>
      </div>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    blogId: ownProps.params.blogId,
    userStore: state.oidc.user,
    postStore: state.postStore
  };
}

const mapDispatchToProps = dispatch =>
  bindActionCreators(postActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
