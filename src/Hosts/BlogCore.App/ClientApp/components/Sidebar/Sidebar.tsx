import * as React from 'react';
import { NavLink } from 'react-router-dom';
import { Badge, Nav, NavItem, NavLink as RsNavLink } from 'reactstrap';
import * as classNames from 'classnames';
import SidebarFooter from './../SidebarFooter/SidebarFooter';
import SidebarForm from './../SidebarForm/SidebarForm';
import SidebarHeader from './../SidebarHeader/SidebarHeader';
import SidebarMinimizer from './../SidebarMinimizer/SidebarMinimizer';
import nav from './_nav';

export default class Sidebar extends React.Component<any, any> {
  shouldComponentUpdate() {
    return false;
  }

  public handleClick(e: any): void {
    e.preventDefault();
    e.target.parentElement.classList.toggle('open');
  }

  public activeRoute(routeName: string, props: any): string {
    return props.location.pathname.indexOf(routeName) > -1
      ? 'nav-item nav-dropdown open'
      : 'nav-item nav-dropdown';
  }

  // todo Sidebar nav secondLevel
  // public secondLevelActive(routeName: string) : string {
  //   return this.props.location.pathname.indexOf(routeName) > -1 ? "nav nav-second-level collapse in" : "nav nav-second-level collapse";
  // }

  public render(): JSX.Element {
    const props = this.props;
    const activeRoute = this.activeRoute;
    const handleClick = this.handleClick;

    // badge addon to NavItem
    const badge = (badge: any): JSX.Element => {
      if (!badge) return <div />;
      const classes = classNames(badge.class);
      return (
        <Badge className={classes} color={badge.variant}>
          {badge.text}
        </Badge>
      );
    };

    // simple wrapper for nav-title item
    const wrapper = (item: any) => {
      return item.wrapper && item.wrapper.element
        ? React.createElement(
            item.wrapper.element,
            item.wrapper.attributes,
            item.name
          )
        : item.name;
    };

    // nav list section title
    const title = (title: any, key: number) => {
      const classes = classNames('nav-title', title.class);
      return (
        <li key={key} className={classes}>
          {wrapper(title)}{' '}
        </li>
      );
    };

    // nav list divider
    const divider = (divider: any, key: number) => (
      <li key={key} className="divider" />
    );

    // nav item with nav link
    const navItem = (item: any, key: number) => {
      const classes = classNames(item.class);
      const variant = classNames(
        'nav-link',
        item.variant ? `nav-link-${item.variant}` : ''
      );
      return (
        <NavItem key={key} className={classes}>
          {/*isExternal(item.url)*/ false ? (
            <RsNavLink href={item.url} className={variant}>
              <i className={item.icon} />
              {item.name}
              {badge(item.badge)}
            </RsNavLink>
          ) : (
            <NavLink to={item.url} className={variant} activeClassName="active">
              <i className={item.icon} />
              {item.name}
              {badge(item.badge)}
            </NavLink>
          )}
        </NavItem>
      );
    };

    // nav dropdown
    const navDropdown = (item: any, key: number) => {
      return (
        <li key={key} className={activeRoute(item.url, props)}>
          <a
            className="nav-link nav-dropdown-toggle"
            href="#"
            onClick={handleClick.bind(this)}
          >
            <i className={item.icon} />
            {item.name}
          </a>
          <ul className="nav-dropdown-items">{navList(item.children)}</ul>
        </li>
      );
    };

    // nav link
    const navLink = (item: any, idx: number) =>
      item.title
        ? title(item, idx)
        : item.divider
          ? divider(item, idx)
          : item.children ? navDropdown(item, idx) : navItem(item, idx);

    // nav list
    const navList = (items: any) => {
      return items.map((item: any, index: number) => navLink(item, index));
    };

    // sidebar-nav root
    return (
      <div className="sidebar">
        <SidebarHeader />
        <SidebarForm />
        <nav className="sidebar-nav">
          <Nav>{navList(nav.items)}</Nav>
        </nav>
        <SidebarFooter />
        <SidebarMinimizer />
      </div>
    );
  }
}
