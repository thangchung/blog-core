import { createStore as _createStore, applyMiddleware } from "redux";
import { routerMiddleware } from "react-router-redux";
import thunkMiddleware from "redux-thunk";
// import loggerMiddleware from "./middleware/loggerMiddleware";
import clientMiddleware from "./middleware/clientMiddleware";
import reducers from "./modules/reducers";
import createHistory from "history/createBrowserHistory";

export const routerHistory = createHistory();

export default function createStore(history) {
  const middlewares = [
    /*loggerMiddleware, */ 
    routerMiddleware(routerHistory),
    thunkMiddleware,
    clientMiddleware
  ];

  const store = _createStore(
    reducers,
    window.__REDUX_DEVTOOLS_EXTENSION__ &&
      window.__REDUX_DEVTOOLS_EXTENSION__(),
    applyMiddleware(...middlewares)
  );
  return store;
}
