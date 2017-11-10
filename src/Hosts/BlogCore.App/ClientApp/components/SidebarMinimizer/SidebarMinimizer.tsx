import * as React from 'react';

export default class SidebarMinimizer extends React.Component<{}, {}> {
  public sidebarMinimize(): void {
    document.body.classList.toggle('sidebar-minimized');
  }

  public brandMinimize(): void {
    document.body.classList.toggle('brand-minimized');
  }

  public render(): JSX.Element {
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
