using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace VideoStreamer.Auth
{
    public class HasResourceHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            if (context.User.HasClaim("resource", context.Resource?.ToString()))
                context.Succeed(requirement);
 
            return Task.CompletedTask;
        }
    }
}