import * as React from 'react';
import { Route, Link } from 'react-router-dom';
import { Breadcrumb, BreadcrumbItem } from 'reactstrap';
import { routeNames as routes } from '../../routes';

const findRouteName = (url: string): string => routes[url];

const getPaths = (pathname: string): string[] => {
  const paths: string[] = ['/'];

  if (pathname === '/') return paths;

  pathname.split('/').reduce((prev: string, curr: string, index: number) => {
    const currPath = `${prev}/${curr}`;
    paths.push(currPath);
    return currPath;
  });
  return paths;
};

const BreadcrumbsItem = ({ match, ...rest }: any): JSX.Element => {
  const routeName: string = findRouteName(match.url);
  if (routeName) {
    return match.isExact ? (
      <BreadcrumbItem active>{routeName}</BreadcrumbItem>
    ) : (
      <BreadcrumbItem>
        <Link to={match.url || ''}>{routeName}</Link>
      </BreadcrumbItem>
    );
  }
  return <div />;
};

const Breadcrumbs = ({
  location: { pathname },
  match,
  ...rest
}: any): JSX.Element => {
  const paths = getPaths(pathname);
  const items = paths.map((path: string, i: number) => (
    <Route key={i++} path={path} component={BreadcrumbsItem} />
  ));

  return <Breadcrumb>{items}</Breadcrumb>;
};

export default (props: {}): JSX.Element => (
  <div>
    <Route path="/:path" component={Breadcrumbs} {...props} />
  </div>
);
