import { Action, Reducer } from "redux";
import { AxiosInstance } from "axios";
import { Observable } from "rxjs";
import { combineEpics, ActionsObservable } from "redux-observable";

import request, { client } from "./../client";
import { CALL_API } from "./../middleware/clientMiddleware";

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
}

interface DeleteBlogSuccessedAction {
  type: "DELETE_BLOG_SUCCESSED";
  id: string;
}

interface DeleteBlogFailedAction {
  type: "DELETE_BLOG_FAILED";
  error: any;
}

interface LoadBlogWrapperAction {
  types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESSED", "LOAD_BLOGS_FAILED"];
  promise: any;
  page: number;
}

interface LoadBlogByIdWrapperAction {
  types: [
    "LOAD_BLOG_BY_ID",
    "LOAD_BLOG_BY_ID_SUCCESSED",
    "LOAD_BLOG_BY_ID_FAILED"
  ];
  promise: any;
}

interface AddBlogWrapperAction {
  types: ["ADD_BLOG", "ADD_BLOG_SUCCESSED", "ADD_BLOG_FAILED"];
  promise: any;
}

interface UpdateBlogWrapperAction {
  types: ["UPDATE_BLOG", "UPDATE_BLOG_SUCCESSED", "UPDATE_BLOG_FAILED"];
  promise: any;
}

interface DeleteBlogWrapperAction {
  types: ["DELETE_BLOG", "DELETE_BLOG_SUCCESSED", "DELETE_BLOG_FAILED"];
  promise: any;
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
    return action$.ofType("LOAD_BLOGS").switchMap(q => {
      console.log(q);
      return Observable.fromPromise(
        request.Blogs.loadBlogsByPage(client, q.page)
      ).map(blogs => actionCreators.loadBlogsByPageSuccessed(blogs));
    });
  },
  (action$: ActionsObservable<LoadBlogByIdAction>): Observable<Action> => {
    return action$.ofType("LOAD_BLOG_BY_ID").switchMap(q => {
      console.log(q);
      return Observable.fromPromise(
        request.Blogs.loadBlogById(client, q.blogId)
      ).map(blog => {
        console.log(blog);
        return actionCreators.loadBlogByIdSuccessed(blog);
      });
    });
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
  addBlog: (blog: any) => {
    return {
      [CALL_API]: {
        payload: <AddBlogWrapperAction>{
          types: ["ADD_BLOG", "ADD_BLOG_SUCCESSED", "ADD_BLOG_FAILED"],
          promise: (client: AxiosInstance) =>
            request.Blogs.createBlog(client, blog)
        }
      }
    };
  },
  updateBlog: (blog: any) => {
    return {
      [CALL_API]: {
        payload: <UpdateBlogWrapperAction>{
          types: ["UPDATE_BLOG", "UPDATE_BLOG_SUCCESSED", "UPDATE_BLOG_FAILED"],
          promise: (client: AxiosInstance) =>
            request.Blogs.editBlog(client, blog)
        }
      }
    };
  },
  deleteBlog: (blogId: string) => {
    return {
      [CALL_API]: {
        payload: <DeleteBlogWrapperAction>{
          types: ["DELETE_BLOG", "DELETE_BLOG_SUCCESSED", "DELETE_BLOG_FAILED"],
          promise: (client: AxiosInstance) =>
            request.Blogs.deleteBlog(client, blogId)
        }
      }
    };
  }
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
      const exhaustiveCheck: never = action;
  }

  return (
    state || {
      loading: true,
      loaded: false,
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
