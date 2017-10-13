import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { userManager } from "../../configs";

class Callback extends React.Component {
  successCallback = (user) => {
    this.props.dispatch(this.props.history.push(`/admin/dashboard`));
  };

  render() {
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
