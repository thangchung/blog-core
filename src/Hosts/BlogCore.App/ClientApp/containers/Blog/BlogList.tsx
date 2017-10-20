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
  Container,
  ButtonGroup,
  Button
} from "reactstrap";

type BlogProps = BlogStore.BlogState &
  typeof BlogStore.actionCreators &
  RouteProps;

class BlogList extends React.Component<BlogProps, any> {
  constructor(props: BlogProps) {
    super(props);
    this.state = {
      selected: {},
      selectAll: 0,
      data: []
    };
    this.toggleRow = this.toggleRow.bind(this);
    this.editRow = this.editRow.bind(this);
    this.deleteRow = this.deleteRow.bind(this);
  }

  componentWillMount() {
    this.props.getBlogsByPage(this.props.page);
  }

  componentDidMount() {
    this.setState({
      data: this.props.blogs
    });
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
      this.state.data.map((x: any) => {
        newSelected[x.id] = true;
      });
    }

    this.setState({
      selected: newSelected,
      selectAll: this.state.selectAll === 0 ? 1 : 0
    });
  }

  public editRow(blog: any): void {
    console.log(blog);
  }

  public deleteRow(id: string): void {
    console.log(id);
  }

  render() {
    // reference at https://codepen.io/aaronschwartz/pen/WOOPRw?editors=0010
    const columns: any = [
      {
        Header: "Blog List",
        columns: [
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
            Header: "Actions",
            Cell: ({ original }: any) => {
              return (
                <div>
                  <ButtonGroup>
                    <Button
                      color="warning"
                      size="lg"
                      onClick={() => this.editRow(original)}
                    >
                      Edit
                    </Button>
                    <Button
                      color="danger"
                      size="lg"
                      onClick={() => this.deleteRow(original.id)}
                    >
                      Delete
                    </Button>
                  </ButtonGroup>
                </div>
              );
            },
            sortable: false,
            width: 120
          }
        ]
      }
    ];

    return (
      <div className="animated fadeIn">
        <Button color="success" size="lg">
          Add
        </Button>
        <ReactTable
          defaultPageSize={5}
          className="-striped -highlight"
          data={this.props.blogs}
          columns={columns}
        />
      </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.blog,
  BlogStore.actionCreators
)(BlogList);
