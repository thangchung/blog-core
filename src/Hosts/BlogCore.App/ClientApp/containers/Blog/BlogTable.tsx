import * as React from 'react';

// ignore TS because react-table doesn't support TS correctly
const ReactTable: any = require('react-table').default;

const columnsRender = (
  selectAll: number,
  selected: any = {},
  toggleSelectAll: () => void,
  toggleRow: (id: string) => void
): any => {
  return [
    {
      id: 'checkbox',
      accessor: '',
      Header: (x: any) => {
        return (
          <input
            key={x.id}
            type="checkbox"
            className="checkbox"
            checked={selectAll === 1}
            ref={input => {
              if (input) {
                input.indeterminate = selectAll === 2;
              }
            }}
            onChange={() => toggleSelectAll()}
          />
        );
      },
      Cell: ({ original }: any) => {
        return (
          <input
            key={original.id}
            type="checkbox"
            className="checkbox"
            checked={selected[original.id] === true}
            onChange={() => toggleRow(original.id)}
          />
        );
      },
      filterable: false,
      sortable: false,
      width: 45
    },
    {
      Header: 'Id',
      accessor: 'id',
      show: false
    },
    {
      Header: 'Title',
      accessor: 'title'
    },
    {
      Header: 'Description',
      accessor: 'description'
    },
    {
      Header: 'Theme',
      accessor: 'theme',
      Cell: (row: any) => (
        <span key={row.id}>
          <span
            style={{
              color: row.value == 1 ? '#57d500' : '#ff2e00',
              transition: 'all .3s ease'
            }}
          >
            &#x25cf;
          </span>{' '}
          {row.value == 1 ? 'Default' : 'Any'}
        </span>
      )
    }
  ];
};

export interface BlogTableProps {
  totalPages: number;
  loading: boolean;
  blogs: any;
  getBlogsByPage(page: number): void;
  history: any;
}

// reference at https://codepen.io/aaronschwartz/pen/WOOPRw?editors=0010
export default class BlogTable extends React.Component<BlogTableProps, any> {
  constructor(props: BlogTableProps) {
    super(props);

    this.state = {
      selected: {},
      selectAll: 0
    };

    // fetch data on every changes on table
    this.fetchData = this.fetchData.bind(this);

    // event for checkbox in the table
    this.toggleRow = this.toggleRow.bind(this);
    this.toggleSelectAll = this.toggleSelectAll.bind(this);
  }

  fetchData(state: any, instance: any): void {
    console.info(instance);
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
      this.props.blogs.map((blog: any) => {
        newSelected[blog.id] = true;
      });
    }

    this.setState({
      selected: newSelected,
      selectAll: this.state.selectAll === 0 ? 1 : 0
    });
  }

  public render(): JSX.Element {
    const columns: any = columnsRender(
      this.state.selectAll,
      this.state.selected,
      this.toggleSelectAll,
      this.toggleRow
    );

    const table = (
      <ReactTable
        columns={columns}
        manual
        data={this.props.blogs}
        className="-striped -highlight"
        defaultPageSize={10}
        showPageSizeOptions={false}
        filterable
        defaultSorted={[
          {
            id: 'title',
            desc: true
          }
        ]}
        pages={this.props.totalPages}
        loading={this.props.loading}
        onFetchData={this.fetchData}
        getTdProps={(state: any, rowInfo: any, column: any, instance: any) => {
          return {
            onClick: (event: any, handleOriginal: any) => {
              this.props.history.replace(`/admin/blog/${rowInfo.original.id}`);
              if (handleOriginal) {
                handleOriginal();
              }
            }
          };
        }}
        previousText={<i className="icon-arrow-left" />}
        nextText={<i className="icon-arrow-right" />}
      />
    );

    return table;
  }
}
