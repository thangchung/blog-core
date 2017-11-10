import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router-dom';
import { Card, CardHeader, CardBody, Button } from 'reactstrap';

import { ApplicationState } from '../../redux/modules';
import * as BlogStore from '../../redux/modules/blog';
import BlogTable from './BlogTable';

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteComponentProps<{ page: number }>;

class BlogManagement extends React.Component<BlogProps, any> {
  constructor(props: BlogProps) {
    super(props);
    this.addRow = this.addRow.bind(this);
  }

  public componentWillMount(): void {
    // this.props.loadBlogsByPage(1);
  }

  public addRow(): any {
    this.props.history.replace('/admin/blog');
  }

  render(): JSX.Element {
    const blogs = this.props.ids.map((id: string, idx: number) => {
      return this.props.blogByIds[id];
    });

    var table = (
      <BlogTable
        totalPages={this.props.totalPages}
        loading={this.props.loading && this.props.addLoading}
        blogs={blogs}
        getBlogsByPage={this.props.loadBlogsByPage}
        history={this.props.history}
      />
    );

    return (
      <div className="animated fadeIn">
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-notebook b-icon" />
              Blog Page
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
          <CardBody className="card-body">{table}</CardBody>
        </Card>
      </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.blog,
  BlogStore.actionCreators
)(BlogManagement);
