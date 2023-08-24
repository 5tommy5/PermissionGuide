using PermissionApi.Models;
using PermissionApi.Services;

namespace PermissionApi.Middleware
{
    public class UserClaimsMiddleware
    {
        private readonly RequestDelegate _next;
        public UserClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TokenService claimsManager, UserClaims requestClaims)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var claims = claimsManager.GetUserClaims(token);

                requestClaims.Permission = claims.Permission;
                requestClaims.Id = claims.Id;
            }

            await _next(context);
        }
    }
}
