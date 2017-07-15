import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

class Home extends Component {
  render() {
    const { match } = this.props;

    return (
      <div>
        <Link
          to={`${match.url}blog/34C96712-2CDF-4E79-9E2F-768CB68DD552`}
        >
          Thang Chung's Blog
        </Link>
      </div>
    );
  }
}

export default connect(null, null)(Home);
