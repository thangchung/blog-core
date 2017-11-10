import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router-dom';
import { Field, reduxForm, InjectedFormProps, FormErrors } from 'redux-form';
import {
  Card,
  CardHeader,
  CardBody,
  Form,
  FormGroup,
  Label,
  Button
} from 'reactstrap';

import { ApplicationState } from '../../redux/modules';
import * as BlogStore from '../../redux/modules/blog';
import {
  TextBoxField,
  NumberField,
  CheckboxField,
  SingleSelectField,
  ConfirmationBox
} from 'OurComponents';

interface ExBlogFormProps extends InjectedFormProps<BlogStore.Blog, any> {
  themes: BlogStore.Theme[];
}

type BlogFormProps = ExBlogFormProps &
  typeof BlogStore.actionCreators &
  RouteComponentProps<{ id?: string }>;

class BlogForm extends React.Component<BlogFormProps, any> {
  constructor(props: BlogFormProps) {
    super(props);
    this.state = {
      canDelete: false,
      showConfirm: false
    };
    this.deleteBlog = this.deleteBlog.bind(this);
    this.onConfirmationCancel = this.onConfirmationCancel.bind(this);
  }

  public componentDidMount(): void {
    console.info('[BLOG]: Initialize data...');
    const { match } = this.props;
    if (match.params && match.params.id) {
      this.props.loadBlogById(match.params.id);
      this.setState({
        canDelete: true
      });
    }
  }

  public deleteBlog(event: React.FormEvent<HTMLFormElement>): void {
    event.preventDefault();
    console.info(`[BLOG]: Delete id#${this.props.match.params.id}`);
    this.props.deleteBlog(this.props.match.params.id as string);
  }

  public onConfirmationCancel(showConfirm: boolean): void {
    this.setState({ showConfirm: showConfirm });
  }

  public render(): JSX.Element {
    const { error, handleSubmit, pristine, reset, submitting } = this.props;
    return (
      <div className="animated fadeIn">
        <ConfirmationBox
          showConfirm={this.state.showConfirm}
          onConfirmed={this.deleteBlog}
          onCancel={this.onConfirmationCancel}
        />
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-arrow-right b-icon" />
              Add a new Blog
            </h3>
          </CardHeader>
          <CardBody className="card-body">
            {error && <strong>{error}</strong>}
            <Form onSubmit={handleSubmit} className="b-form">
              <FormGroup>
                <Label for="title">Title</Label>
                <Field
                  name="title"
                  placeholder="Title"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="description">Description</Label>
                <Field
                  name="description"
                  placeholder="Description"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="theme">Theme</Label>
                <Field
                  name="theme"
                  placeholder="Theme"
                  component={SingleSelectField}
                >
                  {this.props.themes.map(theme => (
                    <option key={theme.value} value={theme.value}>
                      {theme.label}
                    </option>
                  ))}
                </Field>
              </FormGroup>
              <FormGroup>
                <Label for="postsPerPage">Posts per page</Label>
                <Field
                  name="postsPerPage"
                  placeholder="Posts per page"
                  component={NumberField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="daysToComment">Days to comment</Label>
                <Field
                  name="daysToComment"
                  placeholder="Days to comment"
                  component={NumberField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="moderateComments">Moderate comments</Label>
                <Field
                  name="moderateComments"
                  placeholder="Moderate comments"
                  component={CheckboxField}
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
                color="danger"
                disabled={!this.state.canDelete}
                onClick={() => {
                  this.setState({
                    showConfirm: !this.state.showConfirm
                  });
                }}
              >
                <i className="icon-trash b-icon" />Delete
              </Button>&nbsp;
              <Button
                color="warning"
                disabled={!pristine}
                onClick={() => {
                  this.props.history.replace('/admin/blogs');
                }}
              >
                <i className="icon-arrow-left b-icon" />Back
              </Button>
            </Form>
          </CardBody>
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
    errors.title = 'Required.';
  } else if (values.title.length > 30) {
    errors.title = 'Too long.';
  }

  if (!values.theme) {
    errors.theme = 'Required.';
  } else if (values.theme != 1) {
    errors.theme = 'Only supports default theme (1).';
  }

  if (!values.postsPerPage) {
    errors.postsPerPage = 'Required.';
  }

  if (!values.daysToComment) {
    errors.daysToComment = 'Required.';
  }

  if (!values.moderateComments) {
    errors.moderateComments = 'Required.';
  }

  return errors;
};

const initData: any = (state: ApplicationState) => ({
  themes: state.blog.themes,
  initialValues: state.blog.blogSelected || {
    title: 'sample title',
    description: 'sample description',
    theme: 1,
    postsPerPage: 10,
    daysToComment: 5,
    moderateComments: true
  }
});

export default connect(initData, BlogStore.actionCreators)(
  reduxForm<Readonly<BlogStore.Blog>, BlogFormProps>({
    form: 'addNewBlogForm',
    enableReinitialize: true,
    validate,
    onSubmit: (values: any, dispatch: any, props: BlogFormProps): void => {
      const { match } = props;
      if (match.params && !match.params.id) {
        props.addBlog(values);
      } else {
        props.updateBlog(values);
      }
    }
  })(BlogForm)
);
