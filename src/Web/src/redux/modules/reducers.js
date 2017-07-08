import { combineReducers } from "redux";
import { routerReducer } from "react-router-redux";
import { reducer as oidcReducer } from "redux-oidc";
import blogs from "./blogs";

const reducers = {
  routing: routerReducer,
  oidc: oidcReducer,
  blogs
};

export default combineReducers(reducers);
