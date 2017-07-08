import { createStore as _createStore, applyMiddleware } from "redux";
import { browserHistory } from "react-router";
import { routerMiddleware } from "react-router-redux";
import thunkMiddleware from 'redux-thunk';
// import loggerMiddleware from "./middleware/loggerMiddleware";
import reducers from "./modules/reducers";

export default function createStore(history) {
  const middlewares = [/*loggerMiddleware, */ routerMiddleware(browserHistory), thunkMiddleware];

  const store = _createStore(
    reducers,
    window.__REDUX_DEVTOOLS_EXTENSION__ &&
      window.__REDUX_DEVTOOLS_EXTENSION__(),
    applyMiddleware(...middlewares)
  );
  return store;
}
