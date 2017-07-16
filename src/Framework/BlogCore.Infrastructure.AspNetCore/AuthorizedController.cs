using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Infrastructure.AspNetCore
{
    [Authorize]
    public class AuthorizedController : Controller
    {
    }
}