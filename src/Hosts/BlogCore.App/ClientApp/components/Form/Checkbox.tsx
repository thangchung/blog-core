import * as React from 'react';
import { Label, Input as ReactStrapInput, FormFeedback } from 'reactstrap';

export default class Checkbox extends React.Component<any, any> {
  render(): JSX.Element {
    const { touched, error, warning, valid } = this.props.meta;
    return (
      <div>
        <Label check>
          <ReactStrapInput
            type={'checkbox'}
            {...this.props.input}
            valid={valid}
          />{' '}
          {this.props.label}
        </Label>
        {touched &&
          ((error && (
            <FormFeedback className="text-error b-form-feedback">
              {error}
            </FormFeedback>
          )) ||
            (warning && (
              <FormFeedback className="b-form-feedback">{warning}</FormFeedback>
            )))}
      </div>
    );
  }
}
