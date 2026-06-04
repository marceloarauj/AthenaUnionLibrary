namespace Mediator.Mediator
{
    internal class MessageHandlerWrapper<TRequest, TResponse>(IMessageHandler<TRequest, TResponse> handler)
        : IMessageHandlerWrapper
        where TRequest : IRequestMessage<TResponse>
    {
        private readonly IMessageHandler<TRequest, TResponse> _handler = handler;

        public async Task<object?> Handle(object request, CancellationToken cancellationToken)
        {
            return await _handler.Handle((TRequest)request, cancellationToken);
        }
    }
}
