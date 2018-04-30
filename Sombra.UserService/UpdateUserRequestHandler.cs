﻿using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UpdateUserRequestHandler : IAsyncRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateUserRequestHandler(UserContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest message)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (existingUser == null)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    ErrorType = UpdateUserErrorType.NotFound
                };
            }

            var isEmailAddressUniqueHandler = new UserEmailExistsRequestHandler(_context);
            var request = new UserEmailExistsRequest
            {
                CurrentUserKey = message.UserKey,
                EmailAddress = message.EmailAddress
            };

            var response = await isEmailAddressUniqueHandler.Handle(request);
            if (response.EmailExists)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    ErrorType = UpdateUserErrorType.EmailExists
                };
            }

            _context.Entry(existingUser).CurrentValues.SetValues(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new UpdateUserResponse
                {
                    Success = false
                };
            }

            var userUpdatedEvent = _mapper.Map<UserUpdatedEvent>(existingUser);
            await _bus.PublishAsync(userUpdatedEvent);

            return new UpdateUserResponse
            {
                Success = true
            };
        }
    }
}