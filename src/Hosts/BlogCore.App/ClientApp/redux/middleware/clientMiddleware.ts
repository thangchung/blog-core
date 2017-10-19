import axios, { AxiosInstance } from "axios";
import { camelizeKeys } from "humps";
import { globalConfig as GlobalConfig } from "./../../configs";

export default (store: any) => (next: any) => (action: any) => {
  if (typeof action === "function") {
    return action(store.dispatch, store.getState);
  }

  const { promise, types, ...rest } = action; // eslint-disable-line no-redeclare
  if (!promise) {
    return next(action);
  }

  const [REQUEST, SUCCESS, FAILURE]: string[] = types;
  next({ ...rest, type: REQUEST });

  const client: AxiosInstance = axios.create({
    baseURL: GlobalConfig.apiServer,
    timeout: 1000
  });

  if (store.getState().oidc && store.getState().oidc.user) {
    // var token: string = store.getState().oidc.user.access_token;
    // client.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }

  const actionPromise: any = promise(client);
  actionPromise
    .then(
      (result: any) =>
        next({ ...rest, ...camelizeKeys(result), type: SUCCESS }),
      (error: any) => next({ ...rest, error, type: FAILURE })
    )
    .catch((error: any) => {
      console.error("MIDDLEWARE ERROR:", error);
      next({ ...rest, error, type: FAILURE });
    });

  return actionPromise;
};
