using BlogCore.Shared.v1.Blog;
using System.Collections.Generic;

namespace BlogCore.Hosts.Web.Client
{
    public class AppState
    {
        public UserModel UserProfile { get; set; }
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();
    }

    public class UserModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public ProfileModel Profile { get; set; } = new ProfileModel();

        public override string ToString()
        {
            return $"{AccessToken}-{TokenType}-{Scope}";
        }
    }

    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
