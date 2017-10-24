import * as React from "react";
import {
  ButtonGroup,
  Button
} from "reactstrap";

// ignore TS because react-table doesn't support TS correctly
const ReactTable: any = require("react-table").default;

export default class BlogTable extends React.Component<any, any> {
  constructor(props: any) {
    super(props);

    this.state = {
      selected: {},
      selectAll: 0
    };

    // fetch data on every changes on table
    this.fetchData = this.fetchData.bind(this);

    // event for checkbox in the table
    this.toggleRow = this.toggleRow.bind(this);
    this.editRow = this.editRow.bind(this);
    this.deleteRow = this.deleteRow.bind(this);
  }

  fetchData(state: any, instance: any): void {
    this.props.getBlogsByPage(state.page);
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
      this.props.ids.map((id: any) => {
        newSelected[id] = true;
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

  public render(): JSX.Element {
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
        filterable: false,
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
        filterable: false,
        sortable: false,
        width: 50
      }
    ];

    const data = this.props.ids.map((id: string, idx: number) => {
      return this.props.blogByIds[id];
    });

    const table = (
      <ReactTable
        columns={columns}
        manual
        data={data}
        className="-striped -highlight"
        defaultPageSize={5}
        showPageSizeOptions={false}
        filterable
        pages={this.props.totalPages}
        loading={this.props.loading}
        onFetchData={this.fetchData}
      />
    );

    return table;
  }
}