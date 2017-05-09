import React from "react";
import userManager from "../utils/userManager";

const styles = {
  root: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "space-around",
    alignItems: "center",
    flexShrink: 1
  }
};

class LoginPage extends React.Component {
  onLoginButtonClick = event => {
    event.preventDefault();
    userManager.signinRedirect();
  };

  render() {
    return (
      <div style={styles.root}>
        <p>Please log in to continue</p>
        <button onClick={this.onLoginButtonClick}>Login with IdentityServer4</button>
      </div>
    );
  }
}

export default LoginPage;
