import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

export class Restricted extends Component {
  componentWillMount() {
    this.checkAuthentication(this.props);
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.location !== this.props.location) {
      this.checkAuthentication(nextProps);
    }
  }

  checkAuthentication(params) {
    // TODO: we need to re-consider, 
    // because now if we press F5 in any authorized resources 
    // it will redirect you to the /
    if (!params.userStore || !params.userStore.profile) {
      // oops, not logged in, so can't be here!
      params.history.push("/");
    }
  }

  render() {
    const { children, ...rest } = this.props;
    return React.cloneElement(children, rest);
  }
}

function mapStateToProps(state, ownProps) {
  return {
    userStore: state.oidc.user
  };
}

export default connect(mapStateToProps, null)(Restricted);
