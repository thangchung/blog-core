import { Observable, AjaxResponse } from 'rxjs';

const defaultHeaders = {
  //Authorization: "<put here in future>",
  'Content-Type': 'application/json'
};

export const get = (url: string, headers: any = {}): Observable<AjaxResponse> =>
  Observable.ajax.get(url, Object.assign({}, defaultHeaders, headers));

export const post = (
  url: string,
  body: any,
  headers: any = {}
): Observable<AjaxResponse> =>
  Observable.ajax.post(url, body, Object.assign({}, defaultHeaders, headers));

export const put = (
  url: string,
  body: any,
  headers: any = {}
): Observable<AjaxResponse> =>
  Observable.ajax.put(url, body, Object.assign({}, defaultHeaders, headers));

export const del = (url: string, headers: any = {}): Observable<AjaxResponse> =>
  Observable.ajax.delete(url, Object.assign({}, defaultHeaders, headers));
