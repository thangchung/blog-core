using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System;
using BlogCore.AccessControl.UseCases.UpdateUserProfileSetting;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Core;

namespace BlogCore.AccessControl.UseCases
{
    [Route("api/users")]
    public class UserApiController : AuthorizedController
    {
        private readonly ISecurityContext _securityContext;
        private readonly IMediator _eventAggregator;

        public UserApiController(ISecurityContext securityContext, IMediator eventAggregator)
        {
            _securityContext = securityContext;
            _eventAggregator = eventAggregator;
        }

        [HttpPut("{userId}/settings")]
        public async Task<UpdateUserProfileSettingResponse> Put(Guid userId, [FromBody] UpdateUserProfileSettingRequest inputModel)
        {
            return await _eventAggregator.Send(new UpdateUserProfileSettingRequest
            {
                UserId = userId,
                GivenName = inputModel.GivenName,
                FamilyName = inputModel.FamilyName,
                Bio = inputModel.Bio,
                Company = inputModel.Company,
                Location = inputModel.Location
            });
        }

        [HttpPut("{id}/disable")]
        public string DisableProfile(int id)
        {
            return "Disable profile";
        }

        [HttpGet("{id}/photo")]
        public string GetProfilePhoto(int id)
        {
            return "Get profile";
        }

        [HttpPut("{id}/photo")]
        public string ChangeProfilePhoto(int id)
        {
            return "Change profile photo";
        }

        [HttpPost]
        public string Register()
        {
            return "Register";
        }
    }
}