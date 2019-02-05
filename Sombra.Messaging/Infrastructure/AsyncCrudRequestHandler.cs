﻿using System.Threading.Tasks;
using AutoMapper;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Infrastructure
{
    public abstract class AsyncCrudRequestHandler<TRequest, TResponse> : AsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : CrudResponse<TResponse>, new()
    {
        protected AsyncCrudRequestHandler(IMapper mapper) : base(mapper) { }

        public TResponse Error(ErrorType errorType) => CrudResponse<TResponse>.Error(errorType);
        public TResponse Success() => CrudResponse<TResponse>.Success();
    }
}
