import * as React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export const confirmationBox = ({
  showConfirm,
  onConfirmed,
  onCancel
}: any): JSX.Element => {
  return (
    <Modal isOpen={showConfirm} autoFocus={false}>
      <ModalHeader>Confirmation Box</ModalHeader>
      <ModalBody>Are you sure to delete?</ModalBody>
      <ModalFooter>
        <Button color="primary" onClick={e => onConfirmed(e)}>
          Confirm
        </Button>{' '}
        <Button color="secondary" onClick={() => onCancel(false)}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  );
};
