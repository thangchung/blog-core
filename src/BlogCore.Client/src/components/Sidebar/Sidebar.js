import React, { Component } from "react";
import { Link } from "react-router";

class Sidebar extends Component {
  handleClick(e) {
    e.preventDefault();
    e.target.parentElement.classList.toggle("open");
  }

  activeRoute(routeName) {
    return this.props.location.pathname.indexOf(routeName) > -1
      ? "nav-item nav-dropdown open"
      : "nav-item nav-dropdown";
  }

  render() {
    return (
      <div className="sidebar">
        <nav className="sidebar-nav">
          <ul className="nav">
            <li className="nav-item">
              <Link to={"/admin"} className="nav-link" activeClassName="active">
                <i className="icon-speedometer" />
                {" "}
                Home
                {" "}
              </Link>
              <Link to={"/admin/blog-info"} className="nav-link" activeClassName="active">
                <i className="icon-notebook" />
                {" "}
                Blog
                {" "}
                <span className="badge badge-info">NEW</span>
              </Link>
              <Link to={"/"} className="nav-link" activeClassName="active">
                <i className="icon-arrow-left" />
                {" "}
                Dashboard
                {" "}
              </Link>
            </li>
          </ul>
        </nav>
      </div>
    );
  }
}

export default Sidebar;
