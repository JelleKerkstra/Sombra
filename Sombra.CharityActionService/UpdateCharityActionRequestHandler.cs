﻿using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using UserKey = Sombra.CharityActionService.DAL.UserKey;

namespace Sombra.CharityActionService
{
    public class UpdateCharityActionRequestHandler : AsyncCrudRequestHandler<UpdateCharityActionRequest, UpdateCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public override async Task<UpdateCharityActionResponse> Handle(UpdateCharityActionRequest message)
        {
            var charityAction = await _context.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));

            if (charityAction == null)
                return Error(ErrorType.NotFound);

            _context.Entry(charityAction).CurrentValues.SetValues(message);
            _context.UserKeys.RemoveRange(charityAction.UserKeys);
            var mappedKeys = _mapper.Map<List<UserKey>>(message.UserKeys);
            _context.UserKeys.AddRange(mappedKeys);
            charityAction.UserKeys = mappedKeys;

            if (await _context.TrySaveChangesAsync())
            {
                await _bus.PublishAsync(_mapper.Map<CharityActionUpdatedEvent>(charityAction));
                return Success();
            }

            return Error(ErrorType.DatabaseError);
        }
    }
}