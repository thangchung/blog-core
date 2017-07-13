import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

class BlogInfo extends Component {
  componentDidMount() {
    this.props.getBlogInfo("5B1FA7C2-F814-47F2-A2F3-03866F978C49");
  }

  render() {
    <div>
    </div>  
  }
}