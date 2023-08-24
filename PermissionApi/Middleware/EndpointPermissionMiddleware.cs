using Microsoft.AspNetCore.Http.Features;
using PermissionApi.Attributes;
using PermissionApi.Models;

namespace PermissionApi.Middleware
{
    public class EndpointPermissionMiddleware : IMiddleware
    {
        private readonly UserClaims _claims;
        public EndpointPermissionMiddleware(UserClaims claims)
        {
            _claims = claims;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

            CheckIfAllowed(endpoint);

            await next.Invoke(context);

        }

        public void CheckIfAllowed(Endpoint? endpoint)
        {
            var attribute = endpoint?.Metadata.GetMetadata<AllowedForAttribute>();

            if (attribute is not null && !IsAllowed(attribute.AllowedFor))
            {
                throw new UnauthorizedAccessException();
            }
        }

        private bool IsAllowed(IEnumerable<PermissionEnum> rights)
        {
            foreach (var right in rights)
            {
                if (_claims.Permission.HasFlag(right))
                {
                    return true;
                }
            }

            return false;
        }
    }


}
