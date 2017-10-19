import * as React from "react";

export default class SidebarMinimizer extends React.Component<any, any> {
  public sidebarMinimize() {
    document.body.classList.toggle("sidebar-minimized");
  }

  public brandMinimize() {
    document.body.classList.toggle("brand-minimized");
  }

  public render() {
    return (
      <button
        className="sidebar-minimizer"
        type="button"
        onClick={event => {
          this.sidebarMinimize();
          this.brandMinimize();
        }}
      />
    );
  }
}
