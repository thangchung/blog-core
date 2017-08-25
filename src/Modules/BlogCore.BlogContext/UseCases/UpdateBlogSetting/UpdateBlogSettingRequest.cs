using BlogCore.Core;
using MediatR;
using System;

namespace BlogCore.BlogContext.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingRequest : IMessage, IRequest<UpdateBlogSettingResponse>
    {
        public Guid BlogId { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}