import React, { Component } from "react";
import { Input } from "reactstrap";

export default class BcInput extends Component {
  render() {
    return <Input type={this.props.type} {...this.props.input} />;
  }
}
