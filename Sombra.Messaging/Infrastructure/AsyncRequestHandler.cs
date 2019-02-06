using System.Threading.Tasks;
using AutoMapper;

namespace Sombra.Messaging.Infrastructure
{
    public abstract class AsyncRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResponse, new()
    {
        public abstract Task<TResponse> Handle(TRequest message);

        protected TResponse MapMayBeNull<TEntity>(TEntity entity, IMapper mapper)
        {
            return entity != null ? mapper.Map<TResponse>(entity) : new TResponse();
        }
    }
}