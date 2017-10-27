import { Action, Reducer } from "redux";
import { AxiosInstance } from "axios";

const BLOGS_PUBLIC_URL = `/public/api/blogs`;
const BLOGS_URL = `/api/blogs`;

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

export interface BlogState {
  loading: boolean;
  loaded: boolean;
  ids: any;
  blogByIds: any;
  newBlog: Blog;
  error: any;
  page: number;
  totalPages: number;
}

interface LoadBlogAction {
  type: "LOAD_BLOGS";
}

interface LoadBlogSuccessAction {
  type: "LOAD_BLOGS_SUCCESSED";
  data: any;
  page: number;
}

interface LoadBlogFailedAction {
  type: "LOAD_BLOGS_FAILED";
  error: any;
}

interface AddBlogAction {
  type: "ADD_BLOG";
}

interface AddBlogSuccessAction {
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

interface UpdateBlogSuccessAction {
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

interface DeleteBlogSuccessAction {
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

type KnownAction =
  | LoadBlogAction
  | LoadBlogSuccessAction
  | LoadBlogFailedAction
  | AddBlogAction
  | AddBlogSuccessAction
  | AddBlogFailedAction
  | UpdateBlogAction
  | UpdateBlogSuccessAction
  | UpdateBlogFailedAction
  | DeleteBlogAction
  | DeleteBlogSuccessAction
  | DeleteBlogFailedAction;

export const actionCreators = {
  getBlogsByPage: (pageNumber: number) =>
    <LoadBlogWrapperAction>{
      types: ["LOAD_BLOGS", "LOAD_BLOGS_SUCCESSED", "LOAD_BLOGS_FAILED"],
      promise: (client: AxiosInstance) =>
        client.get(`${BLOGS_PUBLIC_URL}?page=${pageNumber + 1}`),
      page: pageNumber
    },
  addBlog: (blog: any) =>
    <AddBlogWrapperAction>{
      types: ["ADD_BLOG", "ADD_BLOG_SUCCESSED", "ADD_BLOG_FAILED"],
      promise: (client: AxiosInstance) => client.post(BLOGS_URL, blog)
    },
  updateBlog: (blog: any) =>
    <UpdateBlogWrapperAction>{
      types: ["UPDATE_BLOG", "UPDATE_BLOG_SUCCESSED", "UPDATE_BLOG_FAILED"],
      promise: (client: AxiosInstance) => client.put(BLOGS_URL, blog)
    },
  deleteBlog: (blogId: string) =>
    <DeleteBlogWrapperAction>{
      types: ["DELETE_BLOG", "DELETE_BLOG_SUCCESSED", "DELETE_BLOG_FAILED"],
      promise: (client: AxiosInstance) =>
        client.delete(`${BLOGS_URL}/${blogId}`)
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
      const blogs = action.data.items;
      return {
        ...state,
        ids: blogs.map((blog: Blog) => blog.id),
        blogByIds: blogs.reduce((obj: any, blog: Blog) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page || 0,
        totalPages: action.data.totalPages
      };

    case "LOAD_BLOGS_FAILED":
      return {
        ...state,
        ids: [],
        blogByIds: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 0
      };

    case "ADD_BLOG":
      return {
        ...state,
        loading: true
      };

    case "ADD_BLOG_SUCCESSED":
      const newBlog = action.data.item as Blog;
      console.info(newBlog);
      return {
        ...state,
        loading: false,
        loaded: true
      };

    case "ADD_BLOG_FAILED":
      return {
        ...state,
        error: action.error,
        loaded: true,
        loading: false
      };

      case "UPDATE_BLOG":
      return {
        ...state,
        loading: true
      };

    case "UPDATE_BLOG_SUCCESSED":
      return {
        ...state,
        loading: false,
        loaded: true
      };

    case "UPDATE_BLOG_FAILED":
      return {
        ...state,
        error: action.error,
        loaded: true,
        loading: false
      };

    case "DELETE_BLOG":
      return {
        ...state,
        loading: true
      };

    case "DELETE_BLOG_SUCCESSED":
      return {
        ...state,
        loading: false,
        loaded: true
      };

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
      newBlog: null,
      error: null,
      page: 0,
      totalPages: 0
    }
  );
};
