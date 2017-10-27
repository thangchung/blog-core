import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { Link, RouteProps, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { Card, CardHeader, CardFooter, CardBlock, Button } from "reactstrap";

import { ApplicationState } from "../../redux/modules";
import * as BlogStore from "../../redux/modules/Blog";
import BlogTable from "./BlogTable";

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteComponentProps<{ page: number }>;

class BlogManagement extends React.Component<BlogProps, any> {
  constructor(props: BlogProps) {
    super(props);

    this.addRow = this.addRow.bind(this);
    this.editBlog = this.editBlog.bind(this);
    this.deleteBlog = this.deleteBlog.bind(this);
  }

  public addRow(): any {
    this.props.history.replace("/admin/new-blog");
  }

  public editBlog(blog: any): void {
    console.log(blog);
  }

  public deleteBlog(id: string): void {
    console.info(`DELETE ITEM #${id}`);
    this.props.deleteBlog(id);
    this.props.getBlogsByPage(this.props.page);
    this.props.history.replace("/admin/blogs");
  }

  render(): JSX.Element {
    const blogs = this.props.ids.map((id: string, idx: number) => {
      return this.props.blogByIds[id];
    });

    var table = (
      <BlogTable
        totalPages={this.props.totalPages}
        loading={this.props.loading}
        blogs={blogs}
        getBlogsByPage={this.props.getBlogsByPage}
        deleteBlog={this.deleteBlog}
        editBlog={this.editBlog}
      />
    );

    return (
      <div className="animated fadeIn">
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-notebook b-icon" />
              Blog Management
            </h3>
            <span className="b-panel-actions">
              <Button
                color="success"
                className="pull-right"
                onClick={this.addRow}
              >
                <i className="icon-plus b-icon" />Add
              </Button>
            </span>
          </CardHeader>
          <CardBlock className="card-body">{table}</CardBlock>
        </Card>
      </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.blog,
  BlogStore.actionCreators
)(BlogManagement);
