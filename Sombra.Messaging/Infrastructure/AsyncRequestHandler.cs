using System.Threading.Tasks;
using AutoMapper;

namespace Sombra.Messaging.Infrastructure
{
    public abstract class AsyncRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResponse, new()
    {
        protected IMapper Mapper;

        protected AsyncRequestHandler(IMapper mapper)
        {
            Mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest message);

        protected TResponse MapMayBeNull<TEntity>(TEntity entity)
        {
            return entity != null ? Mapper.Map<TResponse>(entity) : new TResponse();
        }
    }
}