import { Action, Reducer } from "redux";
//import { push } from "react-router-redux";
import { Observable } from "rxjs";
import { ActionsObservable } from "redux-observable";

import request, { client } from "./../client";
import { actionCreators as commonActionCreators } from "./Common";

const responseData = (payload: any) => payload.data;

export type Blog = {
  id: string;
  title: string;
  description?: string;
  theme?: number;
  image?: string;
  postsPerPage: number;
  daysToComment: number;
  moderateComments: boolean;
};

export type Theme = {
  value: number;
  label: string;
};

export interface BlogState {
  loading: boolean;
  loaded: boolean;
  addLoading: boolean;
  ids: any;
  blogByIds: any;
  blogSelected: Blog | null;
  themes: Theme[];
  error: any;
  page: number;
  totalPages: number;
}

export interface LoadBlogAction {
  type: "LOAD_BLOGS";
  page: number;
}

interface LoadBlogSuccessedAction {
  type: "LOAD_BLOGS_SUCCESSED";
  data: any;
  page: number;
}

interface LoadBlogFailedAction {
  type: "LOAD_BLOGS_FAILED";
  error: any;
}

interface LoadBlogByIdAction {
  type: "LOAD_BLOG_BY_ID";
  blogId: string;
}

interface LoadBlogByIdSuccessedAction {
  type: "LOAD_BLOG_BY_ID_SUCCESSED";
  data: any;
}

interface LoadBlogByIdFailedAction {
  type: "LOAD_BLOG_BY_ID_FAILED";
  error: any;
}

interface AddBlogAction {
  type: "ADD_BLOG";
  blog: any;
}

interface AddBlogSuccessedAction {
  type: "ADD_BLOG_SUCCESSED";
  data: any;
}

interface AddBlogFailedAction {
  type: "ADD_BLOG_FAILED";
  error: any;
}

interface UpdateBlogAction {
  type: "UPDATE_BLOG";
  blog: any;
}

interface UpdateBlogSuccessedAction {
  type: "UPDATE_BLOG_SUCCESSED";
  data: any;
}

interface UpdateBlogFailedAction {
  type: "UPDATE_BLOG_FAILED";
  error: any;
}

interface DeleteBlogAction {
  type: "DELETE_BLOG";
  id: string;
}

interface DeleteBlogSuccessedAction {
  type: "DELETE_BLOG_SUCCESSED";
  data: any;
}

interface DeleteBlogFailedAction {
  type: "DELETE_BLOG_FAILED";
  error: any;
}

export type KnownAction =
  | LoadBlogAction
  | LoadBlogSuccessedAction
  | LoadBlogFailedAction
  | LoadBlogByIdAction
  | LoadBlogByIdSuccessedAction
  | LoadBlogByIdFailedAction
  | AddBlogAction
  | AddBlogSuccessedAction
  | AddBlogFailedAction
  | UpdateBlogAction
  | UpdateBlogSuccessedAction
  | UpdateBlogFailedAction
  | DeleteBlogAction
  | DeleteBlogSuccessedAction
  | DeleteBlogFailedAction;

export const blogEpics: any = [
  (action$: ActionsObservable<LoadBlogAction>): Observable<Action> => {
    return action$
      .ofType("LOAD_BLOGS")
      .debounceTime(100)
      .switchMap(q => {
        return Observable.fromPromise(
          request.Blogs.loadBlogsByPage(client, q.page)
        )
          .map(blogs => {
            return actionCreators.loadBlogsByPageSuccessed(blogs);
          })
          .catch(error =>
            Observable.of(actionCreators.loadBlogsByPageFailed(error))
          );
      });
  },
  (action$: ActionsObservable<LoadBlogByIdAction>): Observable<Action> => {
    return action$
      .ofType("LOAD_BLOG_BY_ID")
      .debounceTime(200)
      .switchMap(q => {
        return Observable.fromPromise(
          request.Blogs.loadBlogById(client, q.blogId)
        )
          .map(blog => actionCreators.loadBlogByIdSuccessed(blog))
          .catch(error =>
            Observable.of(actionCreators.loadBlogByIdFailed(error))
          );
      });
  },
  (action$: ActionsObservable<AddBlogAction>): Observable<Action> => {
    return action$
      .ofType("ADD_BLOG")
      .debounceTime(200)
      .switchMap(q => {
        return Observable.fromPromise(request.Blogs.createBlog(client, q.blog))
          .map(data => actionCreators.addBlogSuccessed(data))
          .catch(error => Observable.of(actionCreators.addBlogFailed(error)));
      })
      .delay(200)
      .mapTo(commonActionCreators.redirectTo("/admin/blogs"));
    //.mapTo(push("/admin/blogs"));
  },
  (action$: ActionsObservable<UpdateBlogAction>): Observable<Action> => {
    return action$
      .ofType("UPDATE_BLOG")
      .debounceTime(200)
      .switchMap(q => {
        return Observable.fromPromise(request.Blogs.editBlog(client, q.blog))
          .map(data => actionCreators.updateBlogSuccessed(responseData(data)))
          .catch(error =>
            Observable.of(actionCreators.updateBlogFailed(error))
          );
      })
      .delay(200)
      .mapTo(commonActionCreators.redirectTo("/admin/blogs"));
  },
  (action$: ActionsObservable<DeleteBlogAction>): Observable<Action> => {
    return action$
      .ofType("DELETE_BLOG")
      .debounceTime(200)
      .switchMap(q => {
        return Observable.fromPromise(request.Blogs.deleteBlog(client, q.id))
          .map(data => actionCreators.deleteBlogSuccessed(responseData(data)))
          .catch(error =>
            Observable.of(actionCreators.deleteBlogFailed(error))
          );
      })
      .delay(200)
      .mapTo(commonActionCreators.redirectTo("/admin/blogs"));
  }
];

export const actionCreators = {
  loadBlogsByPage: (page: number) =>
    <LoadBlogAction>{ type: "LOAD_BLOGS", page },

  loadBlogsByPageSuccessed: (data: any) =>
    <LoadBlogSuccessedAction>{ type: "LOAD_BLOGS_SUCCESSED", ...data },

  loadBlogsByPageFailed: (error: any) =>
    <LoadBlogFailedAction>{ type: "LOAD_BLOGS_FAILED", error },

  loadBlogById: (blogId: string) =>
    <LoadBlogByIdAction>{ type: "LOAD_BLOG_BY_ID", blogId },

  loadBlogByIdSuccessed: (data: any) =>
    <LoadBlogByIdSuccessedAction>{ type: "LOAD_BLOG_BY_ID_SUCCESSED", ...data },

  loadBlogByIdFailed: (error: any) =>
    <LoadBlogByIdFailedAction>{ type: "LOAD_BLOG_BY_ID_FAILED", error },

  addBlog: (blog: any) => <AddBlogAction>{ type: "ADD_BLOG", blog },

  addBlogSuccessed: (data: any) =>
    <AddBlogSuccessedAction>{ type: "ADD_BLOG_SUCCESSED", data },

  addBlogFailed: (error: any) =>
    <AddBlogFailedAction>{ type: "ADD_BLOG_FAILED", error },

  updateBlog: (blog: any) => <UpdateBlogAction>{ type: "UPDATE_BLOG", blog },

  updateBlogSuccessed: (data: any) =>
    <UpdateBlogSuccessedAction>{ type: "UPDATE_BLOG_SUCCESSED", data },

  updateBlogFailed: (error: any) =>
    <UpdateBlogFailedAction>{ type: "UPDATE_BLOG_FAILED", error },

  deleteBlog: (id: string) => <DeleteBlogAction>{ type: "DELETE_BLOG", id },

  deleteBlogSuccessed: (data: any) =>
    <DeleteBlogSuccessedAction>{ type: "DELETE_BLOG_SUCCESSED", data },

  deleteBlogFailed: (error: any) =>
    <DeleteBlogFailedAction>{ type: "DELETE_BLOG_FAILED", error }
};

export const reducer: Reducer<BlogState> = (
  state: BlogState,
  action: KnownAction
) => {
  switch (action.type) {
    case "LOAD_BLOGS":
      return {
        ...state,
        loading: true
      };

    case "LOAD_BLOGS_SUCCESSED":
      console.log(action);
      const data = responseData(action);
      return {
        ...state,
        ids: data.items.map((blog: Blog) => blog.id),
        blogByIds: data.items.reduce((obj: any, blog: Blog) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: data.page || 0,
        totalPages: data.totalPages,
        blogSelected: null
      };

    case "LOAD_BLOGS_FAILED":
      return {
        ...state,
        ids: [],
        blogByIds: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 0,
        blogSelected: null
      };

    case "LOAD_BLOG_BY_ID":
      return {
        ...state,
        blogSelected: null,
        loaded: false,
        loading: true
      };

    case "LOAD_BLOG_BY_ID_SUCCESSED":
      console.log(action.data);
      return {
        ...state,
        blogSelected: action.data,
        loaded: true,
        loading: false
      };

    case "LOAD_BLOG_BY_ID_FAILED":
      return {
        ...state,
        blogSelected: null,
        loaded: true,
        loading: false
      };

    case "ADD_BLOG":
    case "UPDATE_BLOG":
    case "DELETE_BLOG":
      return {
        ...state,
        loading: true
      };

    case "ADD_BLOG_SUCCESSED":
    case "UPDATE_BLOG_SUCCESSED":
      return {
        ...state,
        loading: false,
        loaded: true
      };

    case "DELETE_BLOG_SUCCESSED":
      return {
        ...state,
        loading: false,
        loaded: true
      };

    case "ADD_BLOG_FAILED":
    case "UPDATE_BLOG_FAILED":
    case "DELETE_BLOG_FAILED":
      return {
        ...state,
        error: action.error,
        loaded: true,
        loading: false
      };

    default:
    // const exhaustiveCheck: never = action;
    // console.info(exhaustiveCheck);
  }

  return (
    state || {
      loading: true,
      loaded: false,
      redirectTo: "/",
      ids: [],
      blogByIds: [],
      blogSelected: null,
      themes: [
        {
          value: 1,
          label: "Default"
        }
      ],
      error: null,
      page: 0,
      totalPages: 0
    }
  );
};
