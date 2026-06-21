using Microsoft.AspNetCore.Authorization;

namespace AthenaUnionLibrary.Authorization
{
    public class AthenaPermissionHandler : AuthorizationHandler<AthenaPermissionAttribute>
    {
        protected override Task HandleRequirementAsync
        (
            AuthorizationHandlerContext context,
            AthenaPermissionAttribute requirement
        )
        {
            var hasClaim = context.User.Claims
                .Any(c => c.Type == "permission" && c.Value == requirement.Permission);

            if (hasClaim)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
