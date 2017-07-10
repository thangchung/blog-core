import React from "react";
import { connect } from "react-redux";
import { Button } from "reactstrap";
import userManager from "../../utils/userManager";

class Login extends React.Component {
  onLogoutButtonClicked = e => {
    e.preventDefault();
    userManager.removeUser(); // removes the user data from sessionStorage
  };

  onLoginButtonClick = event => {
    event.preventDefault();
    userManager.signinRedirect();
  };

  render() {
    const { profile } = this.props;
    return (
      <div>
        {!profile &&
          <div>
            <Button color="primary" onClick={this.onLoginButtonClick}>
              Local Identity Provider
            </Button>
          </div>}
        {profile &&
          <div>
            <p>UserID: {profile.sub}</p>
            <p>User name: {profile.name}</p>
            <p>Family name: {profile.family_name}</p>
            <p>Given name: {profile.given_name}</p>
            <p>Email: {profile.email}</p>
            <p>Bio: {profile.bio}</p>
            <p>Company: {profile.company}</p>
            <p>Location: {profile.location}</p>
            <p>BlogID: {profile.blog_id}</p>
          </div>}
        {profile &&
          <Button color="default" onClick={this.onLogoutButtonClicked}>
            Logout
          </Button>}
      </div>
    );
  }
}

function extractProfile(state) {
  if (state.oidc.user) {
    return state.oidc.user.profile;
  }
  return "";
}

function mapStateToProps(state, ownProps) {
  return {
    profile: extractProfile(state)
  };
}

export default connect(mapStateToProps, null)(Login);
