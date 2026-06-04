namespace Mediator.Mediator
{
    public interface IMediator
    {
        Task<ResponseObject> Send<ResponseObject>(IRequestMessage<ResponseObject> request, CancellationToken cancellationToken = default);
    }
}
