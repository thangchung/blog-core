import * as React from "react";
import { Input as ReactStrapInput, FormFeedback } from "reactstrap";

export default class Input extends React.Component<any, any> {
  render(): JSX.Element {
    const { touched, error, warning, valid } = this.props.meta;
    console.log(valid);
    return (
      <div>
        <ReactStrapInput
          type={this.props.type}
          placeholder={this.props.label}
          {...this.props.input}
          valid={`${valid}`}
        />
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
