using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Mediator
{
    public class Mediator(IServiceProvider provider) : IMediator
    {
        private readonly IServiceProvider _provider = provider;

        public async Task<TResponse> Send<TResponse>(IRequestMessage<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            var handlerType = typeof(IMessageHandler<,>)
                .MakeGenericType(requestType, typeof(TResponse));

            var handler = _provider.GetRequiredService(handlerType);

            var wrapperType = typeof(MessageHandlerWrapper<,>)
                .MakeGenericType(requestType, typeof(TResponse));

            var wrapper = (IMessageHandlerWrapper)
                Activator.CreateInstance(wrapperType, handler)!;

            var result = await wrapper.Handle(request, cancellationToken);

            return (TResponse)result!;
        }
    }
}
