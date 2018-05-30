﻿using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.CharityService
{
    public class ApproveCharityRequestHandler : IAsyncRequestHandler<ApproveCharityRequest, ApproveCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public ApproveCharityRequestHandler(CharityContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<ApproveCharityResponse> Handle(ApproveCharityRequest message)
        {
            ExtendedConsole.Log("ApproveCharityRequest received");
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.CharityKey.Equals(message.CharityKey));
            if (charity != null)
            {
                if (charity.IsApproved)
                    return new ApproveCharityResponse
                    {
                        ErrorType = ErrorType.AlreadyActive
                    };

                charity.IsApproved = true;
                await _context.SaveChangesAsync();

                var charityCreatedEvent = _mapper.Map<CharityCreatedEvent>(charity);
                await _bus.PublishAsync(charityCreatedEvent);

                return new ApproveCharityResponse
                {
                    Success = true
                };
            }

            return new ApproveCharityResponse
            {
                ErrorType = ErrorType.NotFound
            };
        }
    }
}