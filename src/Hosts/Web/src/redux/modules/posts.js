const LOAD_POSTS = "bc/post/LOAD_POSTS";
const LOAD_POSTS_SUCCESSED = "bc/post/LOAD_POSTS_SUCCESSED";
const LOAD_POSTS_FAILED = "bc/post/LOAD_POSTS_FAILED";
const LOAD_POST = "bc/post/LOAD_POST";
const LOAD_POST_SUCCESSED = "bc/post/LOAD_POST_SUCCESSED";
const LOAD_POST_FAILED = "bc/post/LOAD_POST_FAILED";

const POSTS_RESOURCE = "public/api/posts";

const initialState = {
  loading: true,
  loaded: false,
  bySlugs: [],
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
      const posts = action.data.items;
      return {
        ...state,
        bySlugs: posts.map(post => post.slug),
        posts: posts.reduce((obj, post) => {
          obj[post.slug] = post;
          return obj;
        }, {}),
        loaded: true,
        loading: false,
        page: action.page
      };

    case LOAD_POSTS_FAILED:
      return {
        ...state,
        bySlugs: [],
        posts: {},
        error: action.error,
        loaded: true,
        loading: false,
        page: 1
      };

    case LOAD_POST:
      return {
        ...state,
        loading: true
      };

    case LOAD_POST_SUCCESSED:
      return {
        ...state,
        post: action.data,
        loaded: true,
        loading: false
      };

    case LOAD_POST_FAILED:
      return {
        ...state,
        post: {},
        loaded: false,
        loading: false
      };

    default:
      return state;
  }
}

export function getPosts(blogId, page) {
  return {
    types: [LOAD_POSTS, LOAD_POSTS_SUCCESSED, LOAD_POSTS_FAILED],
    promise: client =>
      client.get(`public/api/blogs/${blogId}/posts?page=${page}`),
    page
  };
}

export function loadPostById(blogId, postId) {
  return {
    types: [LOAD_POST, LOAD_POST_SUCCESSED, LOAD_POST_FAILED],
    promise: client => client.get(`public/api/blogs/${blogId}/posts/${postId}`)
  };
}

// Reference at https://github.com/reactjs/redux/issues/723
export function loadBlogPostById(blogId, postId) {
  return dispatch => Promise.all([
    dispatch(loadPostById(blogId, postId))
  ]);
}
