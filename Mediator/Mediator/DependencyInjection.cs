using Mediator.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mediator.Mediator
{
    public static class DependencyInjection
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddMediator(Action<MediatorConfiguration> configure)
            {
                var config = new MediatorConfiguration();
                configure(config);

                services.AddScoped<IMediator, Mediator>();

                RegisterHandlers(services, config.Assemblies);

                return services;
            }
        }

        private static void RegisterHandlers(
            IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            var handlerInterface = typeof(IMessageHandler<,>);

            foreach (var assembly in assemblies)
            {
                var handlers = assembly
                    .GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces()
                        .Where(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == handlerInterface),
                        (implementation, service) =>
                            new { service, implementation });

                foreach (var handler in handlers)
                {
                    services.AddScoped(handler.service, handler.implementation);
                }
            }
        }
    }
}
