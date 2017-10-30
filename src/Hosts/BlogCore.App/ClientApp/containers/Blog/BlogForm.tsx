import * as React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps } from "react-router-dom";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
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
import {
  TextBoxField,
  NumberField,
  FormCheckbox,
  SingleSelectField
} from "../../components";

interface ExBlogFormProps extends InjectedFormProps<BlogStore.Blog, any> {
  themes: BlogStore.Theme[];
}

type BlogFormProps = ExBlogFormProps &
  typeof BlogStore.actionCreators &
  RouteComponentProps<any>;

class BlogForm extends React.Component<BlogFormProps, any> {
  public componentDidMount(): void {
    console.info("[BLOG]: Initialize data...");
    const { initialize, match } = this.props;
    const defaultData: any = {
      title: "sample title",
      description: "sample description",
      theme: 1,
      postsPerPage: 10,
      daysToComment: 5,
      moderateComments: true
    };

    if (match.params && match.params.id) {
      Promise.all([
        this.props.getBlogById(match.params.id)
      ]).then((result: any) => {
        const { data } = result[0];
        initialize(data);
      });
    } else {
      initialize(defaultData);
    }
  }

  public render(): JSX.Element {
    console.log(this.props);
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
                <Field
                  placeholder="Title"
                  name="title"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="description">Description</Label>
                <Field
                  placeholder="Description"
                  name="description"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="theme">Theme</Label>
                <Field
                  placeholder="Theme"
                  name="theme"
                  component={SingleSelectField}
                >
                  {this.props.themes.map(theme => (
                    <option value={theme.value}>{theme.label}</option>
                  ))}
                </Field>
              </FormGroup>
              <FormGroup>
                <Label for="postsPerPage">Posts per page</Label>
                <Field
                  placeholder="Posts per page"
                  name="postsPerPage"
                  component={NumberField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="daysToComment">Days to comment</Label>
                <Field
                  placeholder="Days to comment"
                  name="daysToComment"
                  component={NumberField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="moderateComments">Moderate comments</Label>
                <Field
                  placeholder="Moderate comments"
                  name="moderateComments"
                  component={FormCheckbox}
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
                  this.props.history.replace("/admin/blogs");
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

const validate = (
  values: Readonly<BlogStore.Blog>
): FormErrors<BlogStore.Blog> => {
  const errors: FormErrors<BlogStore.Blog> = {};
  if (!values.title) {
    errors.title = "Required.";
  } else if (values.title.length > 30) {
    errors.title = "Too long.";
  }

  if (!values.theme) {
    errors.theme = "Required.";
  } else if (values.theme != 1) {
    errors.theme = "Only supports default theme (1).";
  }

  if (!values.postsPerPage) {
    errors.postsPerPage = "Required.";
  }

  if (!values.daysToComment) {
    errors.daysToComment = "Required.";
  }

  if (!values.moderateComments) {
    errors.moderateComments = "Required.";
  }

  return errors;
};

const initData: any = (state: ApplicationState) => ({
  themes: state.blog.themes
});

export default connect(initData, BlogStore.actionCreators)(
  reduxForm<Readonly<BlogStore.Blog>, BlogFormProps>({
    form: "addNewBlogForm",
    validate,
    onSubmit: (values: any, dispatch: any, props: BlogFormProps): void => {
      const { match } = props;
      if (match.params && !match.params.id) {
        Promise.all([props.addBlog(values)]).then(result => {
          props.history.push("/admin/blogs");
        });
      } else {
        Promise.all([props.updateBlog(values)]).then(result => {
          props.history.push("/admin/blogs");
        });
      }
    }
  })(BlogForm)
);
