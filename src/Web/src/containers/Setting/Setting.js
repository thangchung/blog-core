import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Field, FormSection, reduxForm } from "redux-form";
import { Button, Form, FormGroup, Label, FormFeedback } from "reactstrap";

// import Editor from "react-medium-editor";
// require("medium-editor/dist/css/medium-editor.css");
// require("medium-editor/dist/css/themes/beagle.css");

import * as blogActions from "../../redux/modules/blogs";
import BcInput from "../../components/Form/BcInput";

class ProfileSetting extends React.Component {
  handleUpdateSetting() {
    var settings = {
      ...this.props.profileSetting
    };
    this.props.updateProfileSetting(settings);
  }

  render() {
    return (
      <div>
        <FormGroup>
          <Label for="given_name">Given name</Label>
          <Field
            id="given_name"
            name="given_name"
            type="text"
            component={BcInput}
          />
        </FormGroup>
        <FormGroup>
          <Label for="family_name">Family name</Label>
          <Field
            id="family_name"
            name="family_name"
            type="text"
            component={BcInput}
          />
        </FormGroup>
        <FormGroup>
          <Label for="bio">Bio</Label>
          <Field id="bio" name="bio" type="text" component={BcInput} />
        </FormGroup>
        <FormGroup>
          <Label for="company">Company</Label>
          <Field id="company" name="company" type="text" component={BcInput} />
        </FormGroup>
        <FormGroup>
          <Label for="location">Location</Label>
          <Field
            id="location"
            name="location"
            type="text"
            component={BcInput}
          />
        </FormGroup>
        <Button onClick={() => this.handleUpdateSetting()}>
          Update Profile
        </Button>
      </div>
    );
  }
}

class BlogSetting extends React.Component {
  render() {
    return (
      <div>
        <FormGroup>
          <Label for="dateToComment">Date to comment</Label>
          <Field
            id="dateToComment"
            name="dateToComment"
            type="number"
            component={BcInput}
          />
        </FormGroup>
        <FormGroup>
          <Label for="moderateComment">Moderate comment</Label>
          <Field
            id="moderateComment"
            name="moderateComment"
            type="radio"
            component={BcInput}
          />
        </FormGroup>
        <FormGroup>
          <Label for="postPerPage">Post per page</Label>
          <Field
            id="postPerPage"
            name="postPerPage"
            type="number"
            component={BcInput}
          />
        </FormGroup>
      </div>
    );
  }
}

class Setting extends React.Component {
  /*handleUpdateSetting() {
    var settings = {
      ...this.props.initialValues.blogSetting,
      ...this.props.initialValues.profileSetting
    };
    this.props.updateBlogSetting(settings);
  }*/

  render() {
    const { profile } = this.props;
    return (
      <Form>
        {/*profile &&
          <FormSection name="blogSetting">
            <BlogSetting />
          </FormSection>*/}

        {profile &&
          <FormSection name="profileSetting">
            <ProfileSetting
              profileSetting={this.props.initialValues.profileSetting}
              updateProfileSetting={this.props.updateProfileSetting}
            />
          </FormSection>}

        {/*<div>
            <p>UserID: {profile.sub}</p>
            <p>User name: {profile.name}</p>
            <p>Family name: {profile.family_name}</p>
            <p>Given name: {profile.given_name}</p>
            <p>Email: {profile.email}</p>
            <p>Bio: {profile.bio}</p>
            <p>Company: {profile.company}</p>
            <p>Location: {profile.location}</p>
            <p>BlogID: {profile.blog_id}</p>
          </div>*/}

        {/*<Editor
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
        />*/}

      </Form>
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
    profile: extractProfile(state),
    initialValues: {
      blogSetting: {
        dateToComment: 5,
        moderateComment: true,
        postPerPage: 5
      },
      profileSetting: extractProfile(state) || {}
    }
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(blogActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(
  reduxForm({
    form: "settingForm",
    enableReinitialize: true
  })(Setting)
);
