using MediatR;
using System;

namespace BlogCore.BlogContext.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingRequest : IRequest<UpdateBlogSettingResponse>
    {
        public Guid BlogId { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}