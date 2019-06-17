using BlogCore.Shared.v1.Blog;
using System;
using System.Collections.Generic;

namespace BlogCore.Hosts.Web.Client
{
    public class AppState
    {
        public event Action OnChange;
        public UserInfoModel UserInfo { get; private set; }
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();

        public void SetUserInfo(UserInfoModel userInfo)
        {
            UserInfo = userInfo;
            NotifyStateChanged();
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
