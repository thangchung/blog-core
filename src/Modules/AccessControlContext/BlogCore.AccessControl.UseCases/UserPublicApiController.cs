using BlogCore.AccessControl.Domain.SecurityContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogCore.AccessControl.UseCases
{
    [Route("api/public/users")]
    public class UserPublicApiController : Controller
    {
        private readonly ISecurityContext _securityContext;
        private readonly IMediator _eventAggregator;

        public UserPublicApiController(ISecurityContext securityContext, IMediator eventAggregator)
        {
            _securityContext = securityContext;
            _eventAggregator = eventAggregator;
        }

        [HttpGet("settings")]
        public List<string> Get()
        {
            return new List<string>
            {
                $"Username: {_securityContext.GetCurrentUserName()}",
                $"Email: {_securityContext.GetCurrentEmail()}"
            };
        }
    }
}
