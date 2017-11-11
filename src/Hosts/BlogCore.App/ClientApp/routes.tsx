import * as React from 'react';
import { Route, Switch } from 'react-router-dom';

import { PublicLayout, FullLayout } from './containers';

export const routeNames: { [key: string]: string } = {
  '/admin': 'Dashboard',
  '/admin/blogs': 'Blogs',
  '/admin/blog': 'New blog',
  '/admin/blog/1': 'Update blog'
};

export const routes: any = (
  <Switch>
    <Route path="/admin" component={FullLayout} />
    <Route path="/" component={PublicLayout} />
  </Switch>
);
