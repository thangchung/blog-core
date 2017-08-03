using System.Collections.Generic;
using BlogCore.AccessControl.Domain.SecurityContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;

namespace BlogCore.AccessControl.UseCases
{
    [Authorize]
    [Route("api/users")]
    public class UserApiController : Controller
    {
        private readonly ISecurityContext _securityContext;
        private readonly IMediator _eventAggregator;

        public UserApiController(ISecurityContext securityContext, IMediator eventAggregator)
        {
            _securityContext = securityContext;
            _eventAggregator = eventAggregator;
        }

        [HttpGet("settings"), AllowAnonymous]
        public List<string> Get()
        {
            return new List<string>
            {
                $"Username: {_securityContext.GetCurrentUserName()}",
                $"Email: {_securityContext.GetCurrentEmail()}"
            };
        }

        [HttpPut("settings")]
        public Task<bool> Put(int id)
        {
            /*await _eventAggregator.Send(new UpdateUserProfileSettingRequest
            {
                UserId = userId,
                GivenName = inputModel.GivenName,
                FamilyName = inputModel.FamilyName,
                Bio = inputModel.Bio,
                Company = inputModel.Company,
                Location = inputModel.Location
            });*/

            return Task.FromResult(true);
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