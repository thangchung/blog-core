using BlogCore.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogCore.AccessControlContext.UseCases
{
    [Route("public/api/users")]
    public class UserApiPublicController : Controller
    {
        private readonly ISecurityContext _securityContext;
        private readonly IMediator _eventAggregator;

        public UserApiPublicController(ISecurityContext securityContext, IMediator eventAggregator)
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
