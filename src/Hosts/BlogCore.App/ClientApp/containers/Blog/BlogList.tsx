import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { Link, RouteProps } from "react-router-dom";
import { connect } from "react-redux";
import { ApplicationState } from "../../redux/modules";
import * as BlogStore from "../../redux/modules/Blog";

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteProps;

class BlogList extends React.Component<BlogProps, any> {
  public componentDidMount() {
    let blogs = this.props.getBlogsByPage(1);
    console.log(blogs);
  }

  public render() {
    return <div>Blog List</div>;
  }
}

export default connect(
  (state: ApplicationState) => state.blog,
  BlogStore.actionCreators
)(BlogList);
