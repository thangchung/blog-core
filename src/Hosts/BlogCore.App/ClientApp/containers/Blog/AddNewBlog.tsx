import * as React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Field, reduxForm, FormProps, FormErrors } from "redux-form";
import { RouteComponentProps } from "react-router-dom";
import {
  Card,
  CardHeader,
  CardFooter,
  CardBlock,
  Form,
  FormGroup,
  Label,
  Button
} from "reactstrap";

import { ApplicationState } from "../../redux/modules";
import * as BlogStore from "../../redux/modules/Blog";
import { FormInput } from "../../components";

const validate = (
  values: Readonly<BlogStore.Blog>
): FormErrors<BlogStore.Blog> => {
  const errors: FormErrors<BlogStore.Blog> = {};
  console.log(values);
  if (!values.title) {
    errors.title = "Required.";
  } else if (values.title.length > 10) {
    errors.title = "Too long.";
  }

  return errors;
};

type AddNewBlogFormProps = FormProps<BlogStore.Blog, any, any> &
  typeof BlogStore.actionCreators &
  RouteComponentProps<any>;

class AddNewBlogForm extends React.Component<AddNewBlogFormProps, {}> {
  render(): JSX.Element {
    const { error, handleSubmit, pristine, reset, submitting } = this.props;
    return (
      <div className="animated fadeIn">
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-arrow-right b-icon" />
              Add a new Blog
            </h3>
          </CardHeader>
          <CardBlock className="card-body">
            {error && <strong>{error}</strong>}
            <Form onSubmit={handleSubmit} className="b-form">
              <FormGroup>
                <Label for="title">Title</Label>
                <Field placeholder="Title" name="title" component={FormInput} />
              </FormGroup>
              <FormGroup>
                <Label for="description">Description</Label>
                <Field
                  placeholder="Description"
                  name="description"
                  component={FormInput}
                />
              </FormGroup>
              <Button
                color="primary"
                type="submit"
                disabled={pristine || submitting}
              >
                <i className="icon-paper-plane b-icon" />Save
              </Button>&nbsp;
              <Button
                color="info"
                disabled={pristine || submitting}
                onClick={reset}
              >
                <i className="icon-reload b-icon" />Reset
              </Button>&nbsp;
              <Button
                color="warning"
                disabled={!pristine}
                onClick={() => {
                  this.props.history.replace("/blogs");
                }}
              >
                <i className="icon-arrow-left b-icon" />Back
              </Button>
            </Form>
          </CardBlock>
        </Card>
      </div>
    );
  }
}

export default connect(null, BlogStore.actionCreators)(
  reduxForm<Readonly<BlogStore.Blog>, AddNewBlogFormProps>({
    form: "addNewBlogForm",
    validate,
    onSubmit: (values, dispatch, props) => {
      console.log(props);
      props.addNewBlog(values);
    }
  })(AddNewBlogForm)
);
