import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router";
import { Button } from "reactstrap";
import userManager from "../../utils/userManager";

class Home extends Component {
  render() {
    return (
      <div>
        <h1>Home page</h1>
        
      </div>
    );
  }
}

export default connect(null, null)(Home);
