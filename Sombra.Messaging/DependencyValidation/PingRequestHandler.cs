using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging.DependencyValidation
{
    public class PingRequestHandler<TRequest, TResponse> : AsyncRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : PingResponse, new()
    {
        public override Task<TResponse> Handle(TRequest message)
        {
            return Task.FromResult(new TResponse
            {
                IsOnline = true
            });
        }
    }
}