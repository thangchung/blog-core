import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { Link, RouteProps } from "react-router-dom";
import { connect } from "react-redux";
import ReactTable, { Column } from "react-table";
import { ApplicationState } from "../../redux/modules";
import * as BlogStore from "../../redux/modules/Blog";
import {
  Row,
  Col,
  Card,
  CardHeader,
  CardFooter,
  CardBlock,
} from "reactstrap";

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteProps;

class BlogList extends React.Component<BlogProps, any> {
  componentDidMount() {
    this.props.getBlogsByPage(this.props.page);
  }

  render() {
    const columns: any = [
      {
        Header: "Blog List",
        columns: [
          {
            Header: "Id",
            accessor: "id"
          },
          {
            Header: "Title",
            accessor: "title"
          },
          {
            Header: "Description",
            accessor: "description"
          }
        ]
      }
    ];

    return (
      <div className="animated fadeIn">
        <Row>
          <Col xs="12" sm="12" md="12">
            <Card>
              <CardBlock className="card-body">
                <ReactTable
                  defaultPageSize={10}
                  filterable
                  className="-striped -highlight"
                  data={this.props.blogs}
                  columns={columns}
                />
              </CardBlock>
            </Card>
          </Col>
        </Row>
      </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.blog,
  BlogStore.actionCreators
)(BlogList);
