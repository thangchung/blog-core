import axios from "axios";

const LOAD_BLOGS = "bc/blog/LOAD_BLOGS";
const LOAD_BLOGS_SUCCESS = "bc/blog/LOAD_BLOGS_SUCCESS";
const LOAD_BLOGS_FAILED = "bc/blog/LOAD_BLOGS_FAILED";

const LOAD_BLOGS_BY_PAGE_URL = "blogs";
const UPDATE_SETTING_URL = `http://localhost:8484/api/blogs/setting`;
const UPDATE_PROFILE_SETTING_URL = `http://localhost:8484/api/users`;

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
      const blogs = action.result.data.items;
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
    promise: client => client.get(`${LOAD_BLOGS_BY_PAGE_URL}?page=${page}`)
  };
}

export function updateProfileSetting(settings) {
  return (dispatch, getState) => {
    const token = getState().oidc.user.access_token;
    var request = axios.create({
      baseURL: "http://localhost:8484/api/",
      timeout: 1000,
      headers: { Authorization: `Bearer ${token}` }
    });

    var dto = {
      userId: settings.sub,
      givenName: settings.given_name,
      familyName: settings.family_name,
      bio: settings.bio,
      company: settings.company,
      location: settings.location
    };

    request
      .put(`${UPDATE_PROFILE_SETTING_URL}/${settings.sub}/settings`, dto)
      .then(function(response) {
        console.log(response);
      })
      .catch(function(error) {
        console.log(error);
      });
  };
}

export function updateBlogSetting(settings) {
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
