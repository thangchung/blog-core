using System;
using BlogCore.Core;
using MediatR;

namespace BlogCore.AccessControl.UseCases.UpdateUserProfileSetting
{
    public class UpdateUserProfileSettingRequest : IMessage, IRequest<UpdateUserProfileSettingResponse>
    {
        public Guid UserId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
    }
}