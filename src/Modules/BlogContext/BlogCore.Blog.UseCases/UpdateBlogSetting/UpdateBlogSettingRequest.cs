using System;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingRequest : IMessage, IRequest<UpdateBlogSettingResponse>
    {
        public Guid BlogId { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}