const LOAD_POSTS = "bc/post/LOAD_POSTS";
const LOAD_POSTS_SUCCESSED = "bc/post/LOAD_POSTS_SUCCESSED";
const LOAD_POSTS_FAILED = "bc/post/LOAD_POSTS_FAILED";

const LOAD_POSTS_URL = `http://localhost:8484/api/posts/blog`;

const initialState = {
  loading: true,
  loaded: false,
  byIds: [],
  posts: {},
  error: null
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case LOAD_POSTS:
      return {
        ...state,
        loading: true
      };

    case LOAD_POSTS_SUCCESSED:
      console.log(action);
      const posts = action.posts.postItems.reduce((obj, post) => {
        obj[post.id] = post;
        return obj;
      }, {});
      return {
        ...state,
        byIds: action.posts.postItems.map(post => post.id),
        posts: posts,
        loaded: true,
        loading: false
      };

    case LOAD_POSTS_FAILED:
      return {
        ...state,
        byIds: [],
        posts: {},
        error: action.error,
        loaded: true,
        loading: false
      };

    default:
      return state;
  }
}

export function postsLoading() {
  return { type: LOAD_POSTS };
}

export function loadPosts(posts) {
  return { type: LOAD_POSTS_SUCCESSED, posts };
}

export function getPosts(blogId, page) {
  return (dispatch, getState) => {
    if (!getState()["postStore"]["loaded"]) {
      dispatch(postsLoading());
      return fetch(`${LOAD_POSTS_URL}/${blogId}/${page}`)
        .then(response => response.json())
        .then(posts => dispatch(loadPosts(posts)));
    }
  };
}