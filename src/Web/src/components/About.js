import React, { Component } from "react";
import { connect } from "react-redux";

class About extends Component {
  render() {
    const { profile } = this.props;
    return (
      <div>
        {profile &&
          <div>
            <h5>{profile.given_name} {profile.family_name}</h5>
            <p><a href={`mailto:${profile.email}`}>{profile.name}</a></p>
            <p>{profile.bio}</p>
            <p>{profile.company}</p>
            <p>{profile.location}</p>
          </div>}
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

export default connect(mapStateToProps, null)(About);
