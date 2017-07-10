import React from "react";
import { connect } from "react-redux";

require("medium-editor/dist/css/medium-editor.css");
require("medium-editor/dist/css/themes/beagle.css");

import Editor from "react-medium-editor";

class Setting extends React.Component {
  render() {
    const { profile } = this.props;
    return (
      <div>
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

        <Editor
          tag="pre"
          text="test editor"
          options={{
            toolbar: {
              buttons: [
                "bold",
                "italic",
                "underline",
                "h1",
                "h2",
                "h3",
                "h4",
                "h5",
                "h6"
              ]
            }
          }}
        />
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

export default connect(mapStateToProps, null)(Setting);
