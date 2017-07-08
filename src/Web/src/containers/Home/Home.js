import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router";
import { Button } from "reactstrap";
import userManager from "../../utils/userManager";

class Home extends Component {
  onLogoutButtonClicked = e => {
    e.preventDefault();
    userManager.removeUser(); // removes the user data from sessionStorage
  };

  render() {
    var { profile } = this.props;
    return (
      <div>
        <h1>Home page</h1>
        {profile &&
          <div>
            <h3>Username: {profile.name}</h3>
            <h3>Email: {profile.email} </h3>
          </div>}
        {profile === "" &&
          <Button color="default"><Link to="/login">Login</Link></Button>}
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

export default connect(mapStateToProps, null)(Home);
