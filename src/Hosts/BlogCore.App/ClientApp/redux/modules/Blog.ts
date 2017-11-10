import { Observable } from 'rxjs/Observable';
import { AjaxResponse } from 'rxjs/Observable/dom/AjaxObservable';
import { Action, Reducer } from 'redux';
import { ActionsObservable } from 'redux-observable';

import { get, post, put, del } from './../client';
import { actionCreators as commonActionCreators } from './common';
import { globalConfig as GlobalConfig } from './../../configs';
import { responseData } from 'OurUtils';

const BLOGS_PUBLIC_URL: string = `${GlobalConfig.apiServer}/public/api/blogs`;
const BLOGS_URL: string = `${GlobalConfig.apiServer}/api/blogs`;

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

// Load blogs
const LOAD_BLOGS = 'LOAD_BLOGS';
const LOAD_BLOGS_SUCCESSED = 'LOAD_BLOGS_SUCCESSED';
const LOAD_BLOGS_FAILED = 'LOAD_BLOGS_FAILED';

// Load blog by id
const LOAD_BLOG_BY_ID = 'LOAD_BLOG_BY_ID';
const LOAD_BLOG_BY_ID_SUCCESSED = 'LOAD_BLOG_BY_ID_SUCCESSED';
const LOAD_BLOG_BY_ID_FAILED = 'LOAD_BLOG_BY_ID_FAILED';

// Add blog
const ADD_BLOG = 'ADD_BLOG';
const ADD_BLOG_SUCCESSED = 'ADD_BLOG_SUCCESSED';
const ADD_BLOG_FAILED = 'ADD_BLOG_FAILED';

// Update blog
const UPDATE_BLOG = 'UPDATE_BLOG';
const UPDATE_BLOG_SUCCESSED = 'UPDATE_BLOG_SUCCESSED';
const UPDATE_BLOG_FAILED = 'UPDATE_BLOG_FAILED';

// Delete blog
const DELETE_BLOG = 'DELETE_BLOG';
const DELETE_BLOG_SUCCESSED = 'DELETE_BLOG_SUCCESSED';
const DELETE_BLOG_FAILED = 'DELETE_BLOG_FAILED';

interface LoadBlogAction extends Action {
  type: typeof LOAD_BLOGS;
  page: number;
}

interface LoadBlogSuccessedAction extends Action {
  type: typeof LOAD_BLOGS_SUCCESSED;
  items: any;
  totalPages: number;
  page: number;
}

interface LoadBlogFailedAction {
  type: typeof LOAD_BLOGS_FAILED;
  error: any;
}

interface LoadBlogByIdAction {
  type: typeof LOAD_BLOG_BY_ID;
  blogId: string;
}

interface LoadBlogByIdSuccessedAction {
  type: typeof LOAD_BLOG_BY_ID_SUCCESSED;
  data: any;
}

interface LoadBlogByIdFailedAction {
  type: typeof LOAD_BLOG_BY_ID_FAILED;
  error: any;
}

interface AddBlogAction {
  type: typeof ADD_BLOG;
  blog: any;
}

interface AddBlogSuccessedAction {
  type: typeof ADD_BLOG_SUCCESSED;
  data: any;
}

interface AddBlogFailedAction {
  type: typeof ADD_BLOG_FAILED;
  error: any;
}

interface UpdateBlogAction {
  type: typeof UPDATE_BLOG;
  blog: any;
}

interface UpdateBlogSuccessedAction {
  type: typeof UPDATE_BLOG_SUCCESSED;
  data: any;
}

interface UpdateBlogFailedAction {
  type: typeof UPDATE_BLOG_FAILED;
  error: any;
}

interface DeleteBlogAction {
  type: typeof DELETE_BLOG;
  id: string;
}

interface DeleteBlogSuccessedAction {
  type: typeof DELETE_BLOG_SUCCESSED;
  data: any;
}

interface DeleteBlogFailedAction {
  type: typeof DELETE_BLOG_FAILED;
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

const blogRequest = {
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

export const blogEpics: any = [
  (action$: ActionsObservable<LoadBlogAction>): Observable<Action> => {
    return action$
      .ofType(LOAD_BLOGS)
      .debounceTime(100)
      .switchMap(action =>
        blogRequest
          .loadBlogs(action.page)
          .map(result => {
            return actionCreators.loadBlogsByPageSuccessed(result.response);
          })
          .catch(error =>
            Observable.of(actionCreators.loadBlogsByPageFailed(error))
          )
      );
  },
  (action$: ActionsObservable<LoadBlogByIdAction>): Observable<Action> => {
    return action$.ofType(LOAD_BLOG_BY_ID).switchMap(action =>
      blogRequest
        .loadBlogById(action.blogId)
        .map(result => actionCreators.loadBlogByIdSuccessed(result.response))
        .catch(error => Observable.of(actionCreators.loadBlogByIdFailed(error)))
    );
  },
  (action$: ActionsObservable<AddBlogAction>): Observable<Action> => {
    return action$
      .ofType(ADD_BLOG)
      .switchMap(action =>
        blogRequest
          .createBlog(action.blog)
          .map(result => actionCreators.addBlogSuccessed(result.response))
          .catch(error => Observable.of(actionCreators.addBlogFailed(error)))
      )
      .mapTo(commonActionCreators.redirectTo('/admin/blogs'));
  },
  (action$: ActionsObservable<UpdateBlogAction>): Observable<Action> => {
    return action$
      .ofType(UPDATE_BLOG)
      .switchMap(action =>
        blogRequest
          .editBlog(action.blog)
          .map(result =>
            actionCreators.updateBlogSuccessed(responseData(result.response))
          )
          .catch(error => Observable.of(actionCreators.updateBlogFailed(error)))
      )
      .mapTo(commonActionCreators.redirectTo('/admin/blogs'));
  },
  (action$: ActionsObservable<DeleteBlogAction>): Observable<Action> => {
    return action$
      .ofType(DELETE_BLOG)
      .switchMap(action =>
        blogRequest
          .deleteBlog(action.id)
          .map(result =>
            actionCreators.deleteBlogSuccessed(responseData(result.response))
          )
          .catch(error => Observable.of(actionCreators.deleteBlogFailed(error)))
      )
      .mapTo(commonActionCreators.redirectTo('/admin/blogs'));
  }
];

export const actionCreators = {
  loadBlogsByPage: (page: number): Action =>
    <LoadBlogAction>{ type: LOAD_BLOGS, page },

  loadBlogsByPageSuccessed: (data: any) =>
    <LoadBlogSuccessedAction>{ type: LOAD_BLOGS_SUCCESSED, ...data },

  loadBlogsByPageFailed: (error: any) =>
    <LoadBlogFailedAction>{ type: LOAD_BLOGS_FAILED, error },

  loadBlogById: (blogId: string) =>
    <LoadBlogByIdAction>{ type: LOAD_BLOG_BY_ID, blogId },

  loadBlogByIdSuccessed: (data: any) =>
    <LoadBlogByIdSuccessedAction>{ type: LOAD_BLOG_BY_ID_SUCCESSED, data },

  loadBlogByIdFailed: (error: any) =>
    <LoadBlogByIdFailedAction>{ type: LOAD_BLOG_BY_ID_FAILED, error },

  addBlog: (blog: any) => <AddBlogAction>{ type: ADD_BLOG, blog },

  addBlogSuccessed: (data: any) =>
    <AddBlogSuccessedAction>{ type: ADD_BLOG_SUCCESSED, data },

  addBlogFailed: (error: any) =>
    <AddBlogFailedAction>{ type: ADD_BLOG_FAILED, error },

  updateBlog: (blog: any) => <UpdateBlogAction>{ type: UPDATE_BLOG, blog },

  updateBlogSuccessed: (data: any) =>
    <UpdateBlogSuccessedAction>{ type: UPDATE_BLOG_SUCCESSED, data },

  updateBlogFailed: (error: any) =>
    <UpdateBlogFailedAction>{ type: UPDATE_BLOG_FAILED, error },

  deleteBlog: (id: string) => <DeleteBlogAction>{ type: DELETE_BLOG, id },

  deleteBlogSuccessed: (data: any) =>
    <DeleteBlogSuccessedAction>{ type: DELETE_BLOG_SUCCESSED, data },

  deleteBlogFailed: (error: any) =>
    <DeleteBlogFailedAction>{ type: DELETE_BLOG_FAILED, error }
};

export const reducer: Reducer<BlogState> = (
  state: BlogState,
  action: KnownAction
) => {
  switch (action.type) {
    case LOAD_BLOGS:
      return {
        ...state,
        loading: true
      };

    case LOAD_BLOGS_SUCCESSED:
      console.log(action);
      return {
        ...state,
        ids: action.items.map((blog: Blog) => blog.id),
        blogByIds: action.items.reduce((obj: any, blog: Blog) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page || 0,
        totalPages: action.totalPages,
        blogSelected: null
      };

    case LOAD_BLOGS_FAILED:
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

    case LOAD_BLOG_BY_ID:
      return {
        ...state,
        blogSelected: null,
        loaded: false,
        loading: true
      };

    case LOAD_BLOG_BY_ID_SUCCESSED:
      console.log(action);
      return {
        ...state,
        blogSelected: action.data,
        loaded: true,
        loading: false
      };

    case LOAD_BLOG_BY_ID_FAILED:
      return {
        ...state,
        blogSelected: null,
        loaded: true,
        loading: false
      };

    case ADD_BLOG:
    case UPDATE_BLOG:
    case DELETE_BLOG:
      return {
        ...state,
        loading: true
      };

    case ADD_BLOG_SUCCESSED:
    case UPDATE_BLOG_SUCCESSED:
    case DELETE_BLOG_SUCCESSED:
      return {
        ...state,
        loading: false,
        loaded: true
      };

    case ADD_BLOG_FAILED:
    case UPDATE_BLOG_FAILED:
    case DELETE_BLOG_FAILED:
      return {
        ...state,
        error: action.error,
        loaded: true,
        loading: false
      };

    default:
      const exhaustiveCheck: never = action;
      if (typeof exhaustiveCheck != 'undefined') break;
  }

  return (
    state || {
      loading: true,
      loaded: false,
      redirectTo: '/',
      ids: [],
      blogByIds: [],
      blogSelected: null,
      themes: [
        {
          value: 1,
          label: 'Default'
        }
      ],
      error: null,
      page: 0,
      totalPages: 0
    }
  );
};
