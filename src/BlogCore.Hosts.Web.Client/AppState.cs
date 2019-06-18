using BlogCore.Shared.v1.Blog;
using System;
using System.Collections.Generic;

namespace BlogCore.Hosts.Web.Client
{
    /// <summary>
    /// Ref: https://chrissainty.com/3-ways-to-communicate-between-components-in-blazor
    /// Ref: https://derekworthen.com/posts/blazor-state-management-1-data-binding/
    /// Uses State Container mechanism
    /// </summary>
    public class AppState
    {
        public event Action OnChange;

        public bool IsUserInfoLoading { get; private set; } = true;
        public UserInfoModel UserInfo { get; private set; } = new UserInfoModel();
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();

        public bool IsUserInfoLoaded()
        {
            if(UserInfo != null && UserInfo.AccessToken != null)
            {
                return true;
            }

            return false;
        }

        public void SetUserInfo(UserInfoModel userInfo)
        {
            UserInfo = userInfo;
            NotifyStateChanged();
        }

        public void SetBlogs(IEnumerable<BlogDto> blogs)
        {
            Blogs.Clear();
            Blogs.AddRange(blogs);
            NotifyStateChanged();
        }

        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(UserInfo.AccessToken)) throw new Exception("Invalid token."); 
            return UserInfo.AccessToken;
        }

        public bool IsLogin()
        {
            return !string.IsNullOrEmpty(UserInfo.AccessToken);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class UserInfoModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public UserProfileModel Profile { get; set; } = new UserProfileModel();
    }

    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
