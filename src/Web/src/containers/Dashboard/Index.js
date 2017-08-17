import React from "react";
import { connect } from "react-redux";
import { PieChart } from "react-d3-basic";

class Dashboard extends React.Component {
  render() {
    const chartSeries = [
      {
        field: "blog",
        name: "Number of blogs"
      },
      {
        field: "post",
        name: "Number of posts"
      }
    ];
    const generalChartData = [
      {
        title: "blog",
        number: 2
      },
      {
        title: "post",
        number: 100
      }
    ];
    return (
      <div>
        <h3>Dashboard.</h3>
        <PieChart
          width={800}
          height={500}
          data={generalChartData}
          chartSeries={chartSeries}
          value={d => d.number}
          name={d => d.title}
        />
      </div>
    );
  }
}

export default connect(null, null)(Dashboard);
