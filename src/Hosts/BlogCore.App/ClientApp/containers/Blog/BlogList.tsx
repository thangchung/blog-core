import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { Link, RouteProps, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import ReactTable, { Column } from "react-table";
import {
  Row,
  Col,
  Card,
  CardHeader,
  CardFooter,
  CardBlock,
  Container,
  ButtonGroup,
  Button
} from "reactstrap";

import { ApplicationState } from "../../redux/modules";
import * as BlogStore from "../../redux/modules/Blog";

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteComponentProps<{ page: number }>;

class BlogList extends React.Component<BlogProps, any> {
  constructor(props: BlogProps) {
    super(props);
    this.state = {
      selected: {},
      selectAll: 0
    };

    // event for paging
    this.onPageChange = this.onPageChange.bind(this);

    // event for checkbox in the table
    this.toggleRow = this.toggleRow.bind(this);
    this.addRow = this.addRow.bind(this);
    this.editRow = this.editRow.bind(this);
    this.deleteRow = this.deleteRow.bind(this);
  }

  componentWillMount() {
    this.props.getBlogsByPage(this.props.page);
  }

  onSortedChange(column: any, additive: boolean): void {
    console.log(column);
  }

  onPageChange(page: number): void {
    this.props.getBlogsByPage(page);
  }

  public toggleRow(id: string): void {
    const newSelected = { ...this.state.selected };
    newSelected[id] = !this.state.selected[id];
    this.setState({
      selected: newSelected,
      selectAll: 2
    });
  }

  public toggleSelectAll(): void {
    let newSelected: any = {};

    if (this.state.selectAll === 0) {
      this.props.blogs.map((x: any) => {
        newSelected[x.id] = true;
      });
    }

    this.setState({
      selected: newSelected,
      selectAll: this.state.selectAll === 0 ? 1 : 0
    });
  }

  public addRow(): any {
    this.props.history.replace("/admin/new-blog");
  }

  public editRow(blog: any): void {
    console.log(blog);
  }

  public deleteRow(id: string): void {
    console.log(id);
  }

  render(): JSX.Element {
    // reference at https://codepen.io/aaronschwartz/pen/WOOPRw?editors=0010
    const columns: any = [
      {
        id: "checkbox",
        accessor: "",
        Header: (x: any) => {
          return (
            <input
              type="checkbox"
              className="checkbox"
              checked={this.state.selectAll === 1}
              ref={input => {
                if (input) {
                  input.indeterminate = this.state.selectAll === 2;
                }
              }}
              onChange={() => this.toggleSelectAll()}
            />
          );
        },
        Cell: ({ original }: any) => {
          return (
            <input
              type="checkbox"
              className="checkbox"
              checked={this.state.selected[original.id] === true}
              onChange={() => this.toggleRow(original.id)}
            />
          );
        },
        sortable: false,
        width: 45
      },
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
      },
      {
        Cell: ({ original }: any) => {
          return (
            <div>
              <ButtonGroup>
                <a onClick={() => this.editRow(original)}>
                  <i className="icon-notebook" />
                </a>
                <a onClick={() => this.deleteRow(original.id)}>
                  <i className="icon-trash" />
                </a>
              </ButtonGroup>
            </div>
          );
        },
        sortable: false,
        width: 50
      }
    ];

    const { data, pages, loading } = this.state;
    const table = (
      <ReactTable
        columns={columns}
        data={this.props.blogs}
        className="-striped -highlight"
        defaultPageSize={5}
        showPageSizeOptions={false}
        showFilters={false}
        page={this.props.page}
        loading={this.props.loading}
        onPageChange={this.onPageChange}
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
)(BlogList);
