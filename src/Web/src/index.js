import "babel-polyfill";

// Libs
import React from "react";
import ReactDOM from "react-dom";
import { Switch, Route, browserHistory } from "react-router-dom";
import { Provider } from "react-redux";
import { OidcProvider, loadUser } from "redux-oidc";
import { ConnectedRouter } from "react-router-redux";

// Our components
// import getRoutes from "./routes";
import createStore, { routerHistory } from "./redux/configureStore";
import userManager from "./utils/userManager";
import BlogLayout from "./containers/App/BlogLayout";
import AdminBlogLayout from "./containers/App/AdminBlogLayout";

const store = createStore(browserHistory);

// load the current user into the redux store
loadUser(store, userManager);

ReactDOM.render(
  <Provider store={store} key="provider">
    <OidcProvider store={store} userManager={userManager} key="oidcProvider">
      <ConnectedRouter history={routerHistory}>
        <div>
          <Switch>
            <Route path="/admin" component={AdminBlogLayout} />
            <Route path="/" component={BlogLayout} />
          </Switch>
        </div>
      </ConnectedRouter>
    </OidcProvider>
  </Provider>,
  document.getElementById("root")
);
