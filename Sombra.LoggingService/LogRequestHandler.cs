﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Sombra.Core.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Logging;
using Sombra.Messaging.Responses.Logging;

namespace Sombra.LoggingService
{
    public class LogRequestHandler : AsyncRequestHandler<LogRequest, LogResponse>
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private readonly IMapper _mapper;
        private static readonly string _mongoCollection = "rabbitmq";

        public LogRequestHandler(IMongoDatabase database, IMapper mapper)
        {
            _logCollection = database.GetCollection<LogEntry>(_mongoCollection);
             _mapper = mapper;
        }

        public override async Task<LogResponse> Handle(LogRequest message)
        {
            Expression<Func<LogEntry, bool>> filter = l => true;

            if (message.From.HasValue) filter = filter.And(l => l.MessageCreated.CompareTo(message.From.Value) >= 0);
            if (message.To.HasValue) filter = filter.And(l => l.MessageCreated.CompareTo(message.To.Value) <= 0);
            if (message.MessageTypes.Any()) filter = filter.And(l => message.MessageTypes.Contains(l.MessageType));

            var logs = await _logCollection.Find(filter).Project(l => _mapper.Map<Log>(l)).ToListAsync();

            return new LogResponse
            {
                Logs = logs
            };
        }
    }
}
