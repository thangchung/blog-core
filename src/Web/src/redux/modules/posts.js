import axios from "axios";

const LOAD_POSTS = "bc/post/LOAD_POSTS";
const LOAD_POSTS_SUCCESSED = "bc/post/LOAD_POSTS_SUCCESSED";
const LOAD_POSTS_FAILED = "bc/post/LOAD_POSTS_FAILED";
const LOAD_POST = "bc/post/LOAD_POST";
const LOAD_POST_SUCCESSED = "bc/post/LOAD_POST_SUCCESSED";
const LOAD_POST_FAILED = "bc/post/LOAD_POST_FAILED";
const REDIRECT_TO_SPECIFIC_BLOG = "bc/post/REDIRECT_TO_SPECIFIC_BLOG";

const LOAD_POSTS_URL = `http://localhost:8484/api/posts`;

const initialState = {
  loading: true,
  loaded: false,
  byIds: [],
  posts: [],
  post: {},
  blog: null,
  error: null,
  page: 1
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case LOAD_POSTS:
      return {
        ...state,
        loading: true
      };

    case LOAD_POSTS_SUCCESSED:
      return {
        ...state,
        byIds: action.result.items.map(post => post.id),
        posts: action.result.items.reduce((obj, post) => {
          obj[post.id] = post;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page
      };

    case LOAD_POSTS_FAILED:
      return {
        ...state,
        byIds: [],
        posts: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 1
      };

    case LOAD_POST_SUCCESSED:
      return {
        ...state,
        post: action.post,
        loaded: true,
        loading: false
      };

    case REDIRECT_TO_SPECIFIC_BLOG:
      return {
        ...state,
        blog: action.blog
      };

    default:
      return state;
  }
}

export function postsLoading() {
  return { type: LOAD_POSTS };
}

export function loadPosts(result, page) {
  return { type: LOAD_POSTS_SUCCESSED, result, page };
}

export function postLoading() {
  return { type: LOAD_POST };
}

export function postSuccessed(post) {
  return { type: LOAD_POST_SUCCESSED, post };
}

export function postFailed(error) {
  return { type: LOAD_POST_FAILED, error };
}

export function getPosts(blogId, page) {
  return (dispatch, getState) => {
    //if (!getState()["postStore"]["loaded"]) {
    dispatch(postsLoading());
    return fetch(`${LOAD_POSTS_URL}/blog/${blogId}?page=${page}`)
      .then(response => response.json())
      .then(result => dispatch(loadPosts(result, page)));
    //}
  };
}

export function redirectToSpecificBlog(blog) {
  return { type: REDIRECT_TO_SPECIFIC_BLOG, blog };
}

export function loadPostById(postId) {
  return dispatch =>
    axios
      .get(`${LOAD_POSTS_URL}/${postId}`)
      .then(function(response) {
        dispatch(postSuccessed(response.data));
      })
      .catch(function(error) {
        dispatch(postFailed(error));
      });
}
