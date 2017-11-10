import * as React from 'react';
import { Input as ReactStrapInput, FormFeedback } from 'reactstrap';

export const renderTextBoxField = ({
  input,
  label,
  meta: { touched, error, warning, valid }
}: any): JSX.Element => {
  return (
    <div>
      <ReactStrapInput
        type={'input'}
        placeholder={label}
        {...input}
        valid={valid}
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
};

export const renderNumberField = ({
  input,
  label,
  meta: { touched, error, warning, valid }
}: any): JSX.Element => {
  return (
    <div>
      <ReactStrapInput
        type={'number'}
        placeholder={label}
        {...input}
        valid={valid}
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
};

export const renderSingleSelectField = ({
  input,
  label,
  meta: { touched, error, warning, valid },
  children
}: any): JSX.Element => {
  return (
    <div>
      <ReactStrapInput
        type={'select'}
        placeholder={label}
        {...input}
        valid={valid}
      >
        {children}
      </ReactStrapInput>
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
};
