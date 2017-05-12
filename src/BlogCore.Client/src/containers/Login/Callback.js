import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { browserHistory } from "react-router";
import userManager from "../../utils/userManager";

class Callback extends React.Component {
  successCallback = () => {
    this.props.dispatch(browserHistory.push("/admin"));
  };

  render() {
    // just redirect to '/' in both cases
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={this.successCallback}
        errorCallback={this.successCallback}
      >
        <div>
          Redirecting...
        </div>
      </CallbackComponent>
    );
  }
}

function mapDispatchToProps(dispatch) {
  return {
    dispatch
  };
}

export default connect(null, mapDispatchToProps)(Callback);
