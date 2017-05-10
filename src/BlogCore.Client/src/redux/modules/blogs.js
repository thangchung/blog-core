// import apiRequest from "../../utils/request";

// Actions
const LOAD_BLOG_INFO = "blogcore/blog/LOAD_BLOG_INFO";
const LOAD_BLOG_INFO_SUCCESS = "blogcore/blog/LOAD_BLOG_INFO_SUCCESS";
// const LOAD_BLOG_INFO_FAIL = "blogcore/blog/LOAD_BLOG_INFO_FAIL";

const initialState = {
  loaded: false,
  error: null
};

// Reducer
export default function reducer(state = {}, action = {}) {
  switch (action.type) {
    case LOAD_BLOG_INFO:
      return {
        ...state,
        loading: true
      };

    case LOAD_BLOG_INFO_SUCCESS:
      return {
        ...state,
        blog: action.blog,
        loaded: true,
        loading: false
      };

    default:
      return state;
  }
}

// Action Creators
export function loadBlogInfo(blog) {
  return { type: LOAD_BLOG_INFO_SUCCESS, blog };
}

export function getBlogInfo(id) {
  return dispatch =>
    fetch(`http://localhost:8484/api/blogs/${id}`)
    .then(response => response.json())
    .then(blog =>
      dispatch(loadBlogInfo(blog))
    );
}
