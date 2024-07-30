using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Chinese_Sale.Models;

namespace Chinese_Sale.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userClaims = identity.Claims;
                int useId;
                int.TryParse(userClaims.FirstOrDefault(o => o.Type == "userId")?.Value ?? "", out useId);

                var user = new User
                {
                    Id = useId,
                };

                context.Items["User"] = user;
                await _next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the middleware.");

            }
        }

    }
}
