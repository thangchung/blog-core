import React, { Component } from "react";
import logo from "./logo.svg";
import "./AppLayout.css";

export default class AppLayout extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>BlogCore</h2>
        </div>
        <div className="App-intro">
          {this.props.children}
        </div>
      </div>
    );
  }
}
