import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router";
import { Button } from "reactstrap";
import moment from "moment";
import userManager from "../../utils/userManager";
import * as postActions from "../../redux/modules/posts";
import BlogHeader from "../../components/BlogHeader.js";

class Home extends Component {
  componentDidMount() {
    this.props.getPosts(this.props.userStore.profile.blog_id, 1);
  }

  render() {
    const { loading, byIds, posts } = this.props.postStore;
    if (loading) {
      return <div>Loading...</div>;
    }
    return (
      <div>
        <BlogHeader />
        {byIds.map((id, index) =>
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
        <nav class="blog-pagination">
          <a class="btn btn-outline-primary" href="#">Older</a>
          <a class="btn btn-outline-secondary disabled" href="#">Newer</a>
        </nav>
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

const mapDispatchToProps = dispatch =>
  bindActionCreators(postActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(Home);
