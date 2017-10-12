import React, { Component } from "react";

export default class About extends Component {
  render() {
    const { profile } = this.props;
    if (!profile) {
      return <div />;
    }

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
