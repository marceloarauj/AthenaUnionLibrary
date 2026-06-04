namespace Mediator.Mediator
{
    public interface IMessageHandler<RequestMessage, ResponseObject> where RequestMessage : IRequestMessage<ResponseObject>
    {
        Task<ResponseObject> Handle(RequestMessage request, CancellationToken cancellationToken);
    }
}
