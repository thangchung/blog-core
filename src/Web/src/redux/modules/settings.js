import axios from "axios";

const UPDATE_SETTING_URL = `http://localhost:8484/api/blogs/setting`;
const UPDATE_PROFILE_SETTING_URL = `http://localhost:8484/api/users`;

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