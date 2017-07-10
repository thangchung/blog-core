import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router";

class Header extends Component {
  render() {
    var { isAuth } = this.props;
    return (
      <div className="blog-masthead">
        <div className="container">
          <nav className="nav blog-nav">
            <Link to={"/"} className="nav-link" activeClassName="active">Home</Link>
            <Link to={"/dashboard"} className="nav-link">Dashboard</Link>
            <Link to={"/setting"} className="nav-link">Setting</Link>
            <Link to={"/posts"} className="nav-link">Posts</Link>
            <Link to={"/tags"} className="nav-link">Tags</Link>
            <Link to={"/login"} className="nav-link">Login</Link>
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
