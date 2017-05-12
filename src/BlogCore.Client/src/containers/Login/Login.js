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
      <div>
        <h1>Log in</h1>
        <Button color="primary" onClick={this.onLoginButtonClick}>IS4</Button>
      </div>
    );
  }
}

export default Login;
