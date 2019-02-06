﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UserEmailExistsRequestHandler : AsyncRequestHandler<UserEmailExistsRequest, UserEmailExistsResponse>
    {
        private readonly UserContext _context;

        public UserEmailExistsRequestHandler(UserContext context)
        {
            _context = context;
        }

        public override async Task<UserEmailExistsResponse> Handle(UserEmailExistsRequest message)
        {
            Expression<Func<User, bool>> filter = u => u.EmailAddress.Equals(message.EmailAddress, StringComparison.OrdinalIgnoreCase);
            if (message.CurrentUserKey != default)
                filter = filter.And(u => u.UserKey != message.CurrentUserKey);

            return new UserEmailExistsResponse
            {
                EmailExists = await _context.Users.AnyAsync(filter)
            };
        }
    }
}