// libs
import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Button } from "reactstrap";

// components
import userManager from "../../utils/userManager";
import * as postActions from "../../redux/modules/posts";
import BlogHeader from "../../components/BlogHeader.js";

class PostDetail extends Component {
  render(){
    return (<div>Blog Detail.</div>)
  }
}

function mapStateToProps(state, ownProps) {
  return {
    userStore: state.oidc.user,
    postStore: state.postStore
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(postActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(PostDetail);