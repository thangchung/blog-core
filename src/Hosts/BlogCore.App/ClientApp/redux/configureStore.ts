import {
  createStore,
  applyMiddleware,
  compose,
  combineReducers,
  GenericStoreEnhancer,
  Store,
  StoreEnhancerStoreCreator,
  ReducersMapObject
} from 'redux';
import thunk from 'redux-thunk';
import { createEpicMiddleware } from 'redux-observable';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import { History } from 'history';

import client from './middleware/clientMiddleware';
import * as StoreModule from './modules';
import { ApplicationState, reducers, rootEpic } from './modules';

export default function configureStore(
  history: History,
  initialState?: ApplicationState
) {
  // Build middleware. These are functions that can process the actions before they reach the store.
  const windowIfDefined = typeof window === 'undefined' ? null : window as any;

  // Combine all epics and instantiate the app-wide store instance
  const epicMiddleware = createEpicMiddleware(rootEpic);

  // If devTools is installed, connect to it
  const devToolsExtension =
    windowIfDefined &&
    (windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__ as () => GenericStoreEnhancer);
  const createStoreWithMiddleware: any = compose(
    applyMiddleware(thunk, epicMiddleware, client, routerMiddleware(history)),
    devToolsExtension
      ? devToolsExtension()
      : <S>(next: StoreEnhancerStoreCreator<S>) => next
  )(createStore);

  // Combine all reducers and instantiate the app-wide store instance
  const allReducers = buildRootReducer(reducers);
  const store = createStoreWithMiddleware(allReducers, initialState) as Store<
    ApplicationState
  >;

  // Enable Webpack hot module replacement for reducers
  if (module.hot) {
    module.hot.accept('./modules', () => {
      const nextRootReducer = require<typeof StoreModule>('./modules');
      store.replaceReducer(buildRootReducer(nextRootReducer.reducers));
    });
  }

  return store;
}

function buildRootReducer(allReducers: ReducersMapObject) {
  return combineReducers<ApplicationState>(
    Object.assign({}, allReducers, { routing: routerReducer })
  );
}
