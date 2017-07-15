import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

class Header extends Component {
  render() {
    var { isAuth } = this.props;
    return (
      <div className="blog-masthead">
        <div className="container">
          <nav className="nav blog-nav">
            <Link to={"/"} className="nav-link">
              Home
            </Link>
            {isAuth &&
              <Link to={"/admin/dashboard"} className="nav-link">Dashboard</Link>}
            {isAuth &&
              <Link to={"/admin/setting"} className="nav-link">Setting</Link>}
            {isAuth &&
              <Link to={"/admin/posts"} className="nav-link">Posts</Link>}
            {isAuth &&
              <Link to={"/admin/tags"} className="nav-link">Tags</Link>}
            <Link to={"/login"} className="nav-link">Authentication</Link>
          </nav>
        </div>
      </div>
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
