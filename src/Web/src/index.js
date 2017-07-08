import "babel-polyfill";

// Libs
import React from "react";
import ReactDOM from "react-dom";
import { Router, browserHistory } from "react-router";
import { Provider } from "react-redux";
import { OidcProvider, loadUser } from "redux-oidc";
import { syncHistoryWithStore } from "react-router-redux";

// Our components
import getRoutes from "./routes";
import createStore from "./redux/configureStore";
import userManager from "./utils/userManager";

// Style
import "./index.css";

const store = createStore(browserHistory);

// load the current user into the redux store
loadUser(store, userManager);

ReactDOM.render(
  <Provider store={store} key="provider">
    <OidcProvider store={store} userManager={userManager} key="oidcProvider">
      <Router history={syncHistoryWithStore(browserHistory, store)}>
        {getRoutes(store)}
      </Router>
    </OidcProvider>
  </Provider>,
  document.getElementById("root")
);
