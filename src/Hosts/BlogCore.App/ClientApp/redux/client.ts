// import axios, { AxiosInstance } from "axios";
import { Observable, AjaxResponse } from "rxjs";
import { globalConfig as GlobalConfig } from "./../configs";
import { Blog } from "./modules/Blog";

const BLOGS_PUBLIC_URL = `${GlobalConfig.apiServer}/public/api/blogs`;
const BLOGS_URL = `${GlobalConfig.apiServer}/api/blogs`;

const defaultHeaders = {
  //Authorization: "<put here in future>",
  "Content-Type": "application/json"
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

const Blogs = {
  loadBlogs: (page: number): Observable<AjaxResponse> =>
    get(`${BLOGS_PUBLIC_URL}?page=${page + 1}`),
  loadBlogById: (id: string): Observable<AjaxResponse> =>
    get(`${BLOGS_PUBLIC_URL}/${id}`),
  createBlog: (blog: Blog): Observable<AjaxResponse> => post(BLOGS_URL, blog),
  editBlog: (blog: Blog): Observable<AjaxResponse> =>
    put(`${BLOGS_URL}/${blog.id}`, blog),
  deleteBlog: (blogId: string): Observable<AjaxResponse> =>
    del(`${BLOGS_URL}/${blogId}`)
};

export default {
  Blogs
};
