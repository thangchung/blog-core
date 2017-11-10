import axios, { AxiosInstance } from 'axios';
import { camelizeKeys } from 'humps';
import { globalConfig as GlobalConfig } from './../../configs';

export const CALL_API = 'CALL_API';

export default (store: any) => (next: any) => (action: any) => {
  if (action === undefined) {
    return next(action);
  }

  const callAPI = action[CALL_API];
  if (callAPI === undefined) {
    return next(action);
  }

  const { payload } = callAPI;
  const { promise, types, ...rest } = payload; // eslint-disable-line no-redeclare
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
      (result: any) => {
        console.log({ ...rest, ...camelizeKeys(result), type: SUCCESS });
        return next({ ...rest, ...camelizeKeys(result), type: SUCCESS });
      },
      (error: any) => next({ ...rest, error, type: FAILURE })
    )
    .catch((error: any) => {
      console.error('MIDDLEWARE ERROR:', error);
      next({ ...rest, error, type: FAILURE });
    });

  return actionPromise;
};
