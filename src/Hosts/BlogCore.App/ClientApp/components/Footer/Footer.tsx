import * as React from "react";

export default class Footer extends React.Component {
  public render() {
    return (
      <footer className="app-footer">
        <span>
          <a href="http://coreui.io">CoreUI</a> &copy; 2017 creativeLabs.
        </span>
        <span className="ml-auto">
          Powered by <a href="http://coreui.io">CoreUI</a>
        </span>
      </footer>
    );
  }
}
