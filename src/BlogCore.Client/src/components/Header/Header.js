import React, { Component } from "react";
import { connect } from "react-redux";

class Header extends Component {
  sidebarToggle(e) {
    e.preventDefault();
    document.body.classList.toggle("sidebar-hidden");
  }

  sidebarMinimize(e) {
    e.preventDefault();
    document.body.classList.toggle("sidebar-minimized");
  }

  mobileSidebarToggle(e) {
    e.preventDefault();
    document.body.classList.toggle("sidebar-mobile-show");
  }

  asideToggle(e) {
    e.preventDefault();
    document.body.classList.toggle("aside-menu-hidden");
  }

  render() {
    var { isAuth } = this.props;
    console.log(isAuth);
    return (
      <header className="app-header navbar">
      {isAuth &&
        <button
          className="navbar-toggler mobile-sidebar-toggler d-lg-none"
          onClick={this.mobileSidebarToggle}
          type="button"
        >
          ☰
        </button>
      }
        <a className="navbar-brand" href="#" />
        {isAuth &&
        <ul className="nav navbar-nav d-md-down-none mr-auto">
          <li className="nav-item">
            <a
              className="nav-link navbar-toggler sidebar-toggler"
              onClick={this.sidebarToggle}
              href="#"
            >
              ☰
            </a>
          </li>
        </ul>
        }
      </header>
    );
  }
}

function isAuth(state) {
  if (state.oidc.user) {
    return state.oidc.user != null;
  }
  return false;
}

function mapStateToProps(state, ownProps) {
  return {
    isAuth: isAuth(state)
  };
}

export default connect(mapStateToProps, null)(Header);
