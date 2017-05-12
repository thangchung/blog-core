import React from "react";
import { Button } from 'reactstrap';
import userManager from "../../utils/userManager";

const styles = {
  root: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "space-around",
    alignItems: "center",
    flexShrink: 1
  }
};

class Login extends React.Component {
  onLoginButtonClick = event => {
    event.preventDefault();
    userManager.signinRedirect();
  };

  render() {
    return (
      <div style={styles.root}>
        <p>Please log in to continue</p>
        <Button color="primary" onClick={this.onLoginButtonClick}>Login with IS4</Button>
      </div>
    );
  }
}

export default Login;
