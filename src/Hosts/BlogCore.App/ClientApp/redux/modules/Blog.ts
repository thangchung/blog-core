import { Action, Reducer } from "redux";
import { AxiosInstance } from "axios";

const BLOGS_PUBLIC_RESOURCE = "/public/api/blogs";

export type Blog = {
  id: string;
  title: string;
  description?: string;
  theme?: number;
  image?: string;
};

export interface BlogState {
  loading: boolean;
  loaded: boolean;
  ids: any;
  blogByIds: any;
  blogs: any;
  newBlog: Blog;
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

interface AddNewBlogAction {
  type: "ADD_NEW_BLOG";
}

interface AddNewBlogSuccessAction {
  type: "ADD_NEW_BLOG_SUCCESS";
  data: any;
  page: number;
}

interface AddNewBlogFailedAction {
  type: "ADD_NEW_BLOG_FAILED";
  error: any;
}

interface LoadBlogWrapperAction {
  types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESS", "LOAD_BLOGS_FAILED"];
  promise: any;
  page: number;
}

interface AddNewBlogWrapperAction {
  types: ["ADD_NEW_BLOG", "ADD_NEW_BLOG_SUCCESS", "ADD_NEW_BLOG_FAILED"];
  promise: any;
}

type KnownAction =
  | LoadBlogAction
  | LoadBlogSuccessAction
  | LoadBlogFailedAction
  | AddNewBlogAction
  | AddNewBlogSuccessAction
  | AddNewBlogFailedAction;

export const actionCreators = {
  getBlogsByPage: (pageNumber: number) =>
    <LoadBlogWrapperAction>{
      types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESS", "LOAD_BLOGS_FAILED"],
      promise: (client: AxiosInstance) =>
        client.get(`${BLOGS_PUBLIC_RESOURCE}?page=${pageNumber + 1}`),
      page: pageNumber
    },
  addNewBlog: (blog: any) =>
    <AddNewBlogWrapperAction>{
      types: ["ADD_NEW_BLOG", "ADD_NEW_BLOG_SUCCESS", "ADD_NEW_BLOG_FAILED"],
      promise: (client: AxiosInstance) =>
        client.post(`${BLOGS_PUBLIC_RESOURCE}`, blog)
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
        ids: blogs.map((blog: Blog) => blog.id),
        blogByIdss: blogs.reduce((obj: any, blog: Blog) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        blogs: blogs.map((blog: Blog) => blog),
        loaded: true,
        loading: false,
        page: action.page || 0
      };
    case "LOAD_BLOGS_FAILED":
      return {
        ...state,
        ids: [],
        blogs: {},
        blogByIds: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 0
      };

    case "ADD_NEW_BLOG":
      return {
        ...state,
        loading: true
      };

    case "ADD_NEW_BLOG_SUCCESS":
      const newBlog = action.data.item;
      return {
        ...state,
        blogs: { ...state.blogs, newBlog },
        loading: false,
        loaded: true
      };

    case "ADD_NEW_BLOG_FAILED":
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
      blogs: [],
      blogByIds: [],
      newBlog: null,
      error: null,
      page: 0
    }
  );
};
