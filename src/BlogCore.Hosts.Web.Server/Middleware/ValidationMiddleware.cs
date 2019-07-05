using BlogCore.Shared.v1.ValidationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Server.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationMiddleware> _logger;

        public ValidationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValidationMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex.ValidationResultModel);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationResultModel validationResult)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(validationResult.ToString());
        }
    }
}
