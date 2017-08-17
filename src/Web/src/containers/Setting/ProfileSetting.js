import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Field, reduxForm } from "redux-form";
import { Button, Form, FormGroup, Label } from "reactstrap";
import BcInput from "../../components/Form/BcInput";
import * as settingActions from "../../redux/modules/settings";

class ProfileSetting extends React.Component {
  handleUpdateSetting(values) {
    this.props.updateProfileSetting(values);
  }

  render() {
    const { handleSubmit, pristine, reset, submitting } = this.props;
    return (
      <div>
        <Form onSubmit={handleSubmit(this.handleUpdateSetting.bind(this))}>
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
            <Field
              id="company"
              name="company"
              type="text"
              component={BcInput}
            />
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
          <FormGroup>
            <Button
              color="primary"
              type="submit"
              disabled={pristine || submitting}
            >
              Update Profile
            </Button>
            <Button
              type="button"
              disabled={pristine || submitting}
              onClick={reset}
            >
              Reset
            </Button>
          </FormGroup>
        </Form>
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
    initialValues: extractProfile(state) || {}
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(settingActions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(
  reduxForm({
    form: "profileSettingForm",
    enableReinitialize: true
  })(ProfileSetting)
);
