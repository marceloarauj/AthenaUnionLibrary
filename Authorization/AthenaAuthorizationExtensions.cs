using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AthenaUnionLibrary.Authorization
{
    public static class AthenaAuthorizationExtensions
    {
        public static IServiceCollection AddAthenaAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, AthenaPermissionHandler>();
            return services;
        }
    }
}
