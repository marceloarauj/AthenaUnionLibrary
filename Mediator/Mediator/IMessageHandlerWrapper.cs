namespace Mediator.Mediator
{
    internal interface IMessageHandlerWrapper
    {
        Task<object?> Handle(object request, CancellationToken cancellationToken);
    }
}
