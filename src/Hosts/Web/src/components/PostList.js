import React, { Component } from "react";
import moment from "moment";
import { Link } from "react-router-dom";

export default class PostList extends Component {
  render() {
    const { blogId, slugs, posts } = this.props;
    return (
      <div>
        {slugs.map((slug, index) =>
          <div key={slug} className="blog-post">
            <h2 className="blog-post-title">
              <Link to={`/blogs/${blogId}/posts/${posts[slug].id}`}>
                {posts[slug].title}
              </Link>
            </h2>
            <p className="blog-post-meta">
              {moment(posts[slug].created_at).format(
                "MMMM Do, YYYY"
              )}&nbsp;by&nbsp;
              <a href="#">
                {`${posts[slug].author.givenName} ${posts[slug].author.familyName}`}
              </a>
            </p>

            <p>{posts[slug].excerpt}</p>
          </div>
        )}
      </div>
    );
  }
}
