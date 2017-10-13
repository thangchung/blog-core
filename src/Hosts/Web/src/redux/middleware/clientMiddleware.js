import axios from "axios";
import { camelizeKeys } from 'humps';
import { globalConfig } from "../../configs";

export default store => next => action => {
  if (typeof action === "function") {
    return action(store.dispatch, store.getState);
  }

  const { promise, types, ...rest } = action; // eslint-disable-line no-redeclare
  if (!promise) {
    return next(action);
  }

  const [REQUEST, SUCCESS, FAILURE] = types;
  next({ ...rest, type: REQUEST });

  const client = axios.create({
    baseURL: globalConfig.apiServer,
    timeout: 1000
  });

  if (store.getState().oidc.user) {
    var token = store.getState().oidc.user.access_token;
    client.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  }

  const actionPromise = promise(client);
  actionPromise
    .then(
      result => next({ ...rest, ...camelizeKeys(result), type: SUCCESS }),
      error => next({ ...rest, error, type: FAILURE })
    )
    .catch(error => {
      console.error("MIDDLEWARE ERROR:", error);
      next({ ...rest, error, type: FAILURE });
    });

  return actionPromise;
};
