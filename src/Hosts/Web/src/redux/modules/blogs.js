const LOAD_BLOGS = "bc/blog/LOAD_BLOGS";
const LOAD_BLOGS_SUCCESS = "bc/blog/LOAD_BLOGS_SUCCESS";
const LOAD_BLOGS_FAILED = "bc/blog/LOAD_BLOGS_FAILED";

const BLOGS_PUBLIC_RESOURCE = "/public/api/blogs";

const initialState = {
  loading: true,
  loaded: false,
  byIds: [],
  blogs: [],
  error: null,
  page: 1
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case LOAD_BLOGS:
      return {
        ...state,
        loading: true
      };

    case LOAD_BLOGS_SUCCESS:
      const blogs = action.data.items;
      return {
        ...state,
        byIds: blogs.map(blog => blog.id),
        blogs: blogs.reduce((obj, blog) => {
          obj[blog.id] = blog;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page || 1
      };

    case LOAD_BLOGS_FAILED:
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
      return state;
  }
}

export function getBlogsByPage(page) {
  return {
    types: [LOAD_BLOGS, LOAD_BLOGS_SUCCESS, LOAD_BLOGS_FAILED],
    promise: client => client.get(`${BLOGS_PUBLIC_RESOURCE}?page=${page}`),
    page
  };
}
