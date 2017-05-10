import { combineReducers } from "redux";
import { routerReducer } from "react-router-redux";
import { reducer as oidcReducer } from "redux-oidc";

const reducers = {
  routing: routerReducer,
  oidc: oidcReducer
};

export default combineReducers(reducers);
