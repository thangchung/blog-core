import 'babel-polyfill';

// Styles
import 'font-awesome/css/font-awesome.min.css';
import 'simple-line-icons/css/simple-line-icons.css';
import 'react-table/react-table.css';

// Import Main styles for this application
import './scss/style.scss';
import './scss/core/_dropdown-menu-right.scss';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';

import configureStore from './redux/configureStore';
import { ApplicationState } from './redux/modules';
import * as RoutesModule from './routes';
import registerServiceWorker from './registerServiceWorker';

let routes = RoutesModule.routes;

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = (window as any).initialReduxState as ApplicationState;
const store = configureStore(history, initialState);

function renderApp() {
  // This code starts up the React app when it runs in a browser. It sets up the routing configuration
  // and injects the app into a DOM element.
  ReactDOM.hydrate(
    <AppContainer>
      <Provider store={store}>
        <ConnectedRouter history={history} children={routes} />
      </Provider>
    </AppContainer>,
    document.getElementById('react-app')
  );
}

renderApp();
registerServiceWorker();

// Allow Hot Module Replacement
if (module.hot) {
  module.hot.accept('./routes', () => {
    routes = require<typeof RoutesModule>('./routes').routes;
    renderApp();
  });
}
