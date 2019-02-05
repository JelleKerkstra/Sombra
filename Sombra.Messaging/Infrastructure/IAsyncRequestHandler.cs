using System.Threading.Tasks;

namespace Sombra.Messaging.Infrastructure
{
    internal interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResponse, new()
    {
        Task<TResponse> Handle(TRequest message);
    }
}
