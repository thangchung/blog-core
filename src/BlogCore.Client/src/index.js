import "babel-polyfill";

// Libs
import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { OidcProvider, loadUser } from "redux-oidc";
import { createStore, applyMiddleware } from "redux";
import { Router, Route, IndexRoute, browserHistory } from "react-router";
import { syncHistoryWithStore, routerMiddleware } from "react-router-redux";

// Our components
import reducers from "./reducers";
import userManager from "./utils/userManager";
import loggerMiddleware from './utils/loggerMiddleware';
import AppLayout from "./App/AppLayout";
import HomeComponent from "./Home/HomeComponent";
import LoginPage from "./User/LoginPage";
import CallbackPage from "./User/CallbackPage";
import NotFoundComponent from "./Shared/NotFoundComponent";

// Style
import "./index.css";

const middlewares = [loggerMiddleware, routerMiddleware(browserHistory)];

const store = createStore(
  reducers,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__(),
  applyMiddleware(...middlewares)
);

// load the current user into the redux store
loadUser(store, userManager);

const history = syncHistoryWithStore(browserHistory, store);

ReactDOM.render(
  <Provider store={store}>
    <OidcProvider store={store} userManager={userManager}>
      <Router history={history}>
        <Route path="/" component={AppLayout}>
          <IndexRoute component={HomeComponent} />
        </Route>
        <Route path="/login">
          <IndexRoute component={LoginPage} />
        </Route>
        <Route path="/callback">
          <IndexRoute component={CallbackPage} />
        </Route>
        <Route path="*" component={NotFoundComponent} />
      </Router>
    </OidcProvider>
  </Provider>,
  document.getElementById("root")
);
