// import apiRequest from "../../utils/request";
import axios from "axios";

// Actions
const LOAD_BLOGS = "bc/blog/LOAD_BLOGS";
const LOAD_BLOGS_SUCCESS = "bc/blog/LOAD_BLOGS_SUCCESS";
const LOAD_BLOGS_FAILED = "bc/blog/LOAD_BLOGS_FAILED";

const LOAD_BLOGS_BY_PAGE_URL = `http://localhost:8484/api/blogs`;
const UPDATE_SETTING_URL = `http://localhost:8484/api/blogs/setting`;

const initialState = {
  loading: true,
  loaded: false,
  byIds: [],
  blogs: [],
  error: null,
  page: 1
};

// Reducer
export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case LOAD_BLOGS:
      return {
        ...state,
        loading: true
      };

    case LOAD_BLOGS_SUCCESS:
      return {
        ...state,
        byIds: action.blogs.map(blog => blog.id),
        blogs: action.blogs.reduce((obj, blog) => {
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

// Action Creators
export function loadBlogsByPage(blogs) {
  return { type: LOAD_BLOGS_SUCCESS, blogs };
}

export function getBlogsByPage(page) {
  return dispatch =>
    fetch(`${LOAD_BLOGS_BY_PAGE_URL}?page=${page}`)
      .then(response => response.json())
      .then(result => dispatch(loadBlogsByPage(result.items)));
}

export function updateBlogSetting(settings) {
  console.log(settings);
  var dto = {
    givenName: settings.given_name,
    familyName: settings.family_name,
    bio: settings.bio,
    company: settings.company,
    location: settings.location,
    blogId: settings.blog_id,
    postsPerPage: settings.postPerPage,
    daysToComment: settings.dateToComment,
    moderateComments: settings.moderateComment
  };
  axios
    .put(`${UPDATE_SETTING_URL}/${settings.sub}`, dto)
    .then(function(response) {
      console.log(response);
    })
    .catch(function(error) {
      console.log(error);
    });
}
