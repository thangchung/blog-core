import * as React from 'react';
import {
  Badge,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  NavDropdown
} from 'reactstrap';

interface HeaderState {
  dropdownOpen: boolean;
  caretVisible: boolean;
}

export default class Header extends React.Component<{}, HeaderState> {
  constructor(props: {}) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = {
      dropdownOpen: false,
      caretVisible: false
    };
  }

  public toggle(): void {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  }

  public dropAccnt(): JSX.Element {
    return (
      <NavDropdown isOpen={this.state.dropdownOpen} toggle={this.toggle}>
        <DropdownToggle nav caret={this.state.caretVisible}>
          <img
            src={'img/avatars/6.jpg'}
            className="img-avatar"
            alt="admin@bootstrapmaster.com"
          />
          <span className="d-md-down-none">admin</span>
        </DropdownToggle>
        <DropdownMenu right>
          <DropdownItem header tag="div" className="text-center">
            <strong>Account</strong>
          </DropdownItem>
          <DropdownItem>
            <i className="fa fa-bell-o" /> Updates<Badge color="info">42</Badge>
          </DropdownItem>
          <DropdownItem>
            <i className="fa fa-envelope-o" /> Messages<Badge color="success">42</Badge>
          </DropdownItem>
          <DropdownItem header tag="div" className="text-center">
            <strong>Settings</strong>
          </DropdownItem>
          <DropdownItem>
            <i className="fa fa-user" /> Profile
          </DropdownItem>
          <DropdownItem>
            <i className="fa fa-wrench" /> Settings
          </DropdownItem>
          <DropdownItem divider />
          <DropdownItem>
            <i className="fa fa-lock" /> Logout
          </DropdownItem>
        </DropdownMenu>
      </NavDropdown>
    );
  }

  public render(): JSX.Element {
    return this.dropAccnt();
  }
}
