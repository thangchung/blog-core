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

        public UserInfoModel UserInfo { get; private set; } = new UserInfoModel();
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();

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
            return !string.IsNullOrEmpty(UserInfo.AccessToken) && !UserInfo.Expired;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class UserInfoModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public bool Expired { get; set; }
        public UserProfileModel Profile { get; set; } = new UserProfileModel();
    }

    public class UserProfileModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}
