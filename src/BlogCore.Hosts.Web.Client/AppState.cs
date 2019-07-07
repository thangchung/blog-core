using BlogCore.Shared.v1.Blog;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ContentHeaderModel ContentHeader { get; private set; } = new ContentHeaderModel();
        public UserInfoModel UserInfo { get; private set; } = new UserInfoModel();
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();

        public void SetUserInfo(UserInfoModel userInfo)
        {
            UserInfo = userInfo;
            NotifyStateChanged();
        }

        public void SetContentHeader(string contentHeader, IEnumerable<BreadcrumbItem> breadcrumbItems)
        {
            ContentHeader = new ContentHeaderModel {
                ContentHeader = contentHeader,
                BreadcrumbItems = breadcrumbItems.ToList()
            };
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

    public class ContentHeaderModel
    {
        public string ContentHeader { get; set; } = "Dashboard";
        public List<BreadcrumbItem> BreadcrumbItems { get; set; }
        public ContentHeaderModel()
        {
            BreadcrumbItems = new List<BreadcrumbItem> {
                new BreadcrumbItem { Text = "Home", Route = "#" },
                new BreadcrumbItem{ Text = "Dashboard", Route = "dashboard" }
            };
        }
    }

    public class BreadcrumbItem
    {
        public string Text { get; set; }
        public string Route { get; set; }
    }
}
