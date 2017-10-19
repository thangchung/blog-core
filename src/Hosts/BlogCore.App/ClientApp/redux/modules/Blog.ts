import { Action, Reducer } from "redux";
import { AxiosInstance } from "axios";

const BLOGS_PUBLIC_RESOURCE = "/public/api/blogs";

export interface BlogState {
  loading: boolean;
  loaded: boolean;
  byIds: any;
  blogs: any;
  error: any;
  page: number;
}

interface LoadBlogAction {
  type: "LOAD_BLOGS";
}

interface LoadBlogSuccessAction {
  type: "LOAD_BLOGS_SUCCESS";
  data: any;
  page: number;
}

interface LoadBlogFailedAction {
  type: "LOAD_BLOGS_FAILED";
  error: any;
}

type KnownAction =
  | LoadBlogAction
  | LoadBlogSuccessAction
  | LoadBlogFailedAction;

interface LoadBlogWrapperAction {
  types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESS", "LOAD_BLOGS_FAILED"];
  promise: any;
  page: number;
}

export const actionCreators = {
  getBlogsByPage: (pageNumber: number) =>
    <LoadBlogWrapperAction>{
      types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESS", "LOAD_BLOGS_FAILED"],
      promise: (client: AxiosInstance) =>
        client.get(`${BLOGS_PUBLIC_RESOURCE}?page=${pageNumber}`),
      page: pageNumber
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
    case "LOAD_BLOGS_SUCCESS":
      const blogs = action.data.items;
      return {
        ...state,
        byIds: blogs.map((blog: any) => blog.id),
        blogs: blogs.reduce((obj: any, blog: any) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page || 1
      };
    case "LOAD_BLOGS_FAILED":
      return {
        ...state,
        byIds: [],
        blogs: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 1
      };
    default:
      const exhaustiveCheck: never = action;
  }

  return (
    state || {
      loading: true,
      loaded: false,
      byIds: [],
      blogs: [],
      error: null,
      page: 1
    }
  );
};
