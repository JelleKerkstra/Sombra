﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class GetUserByEmailRequestHandler : AsyncRequestHandler<GetUserByEmailRequest, GetUserByEmailResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public GetUserByEmailRequestHandler(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<GetUserByEmailResponse> Handle(GetUserByEmailRequest message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress.Equals(message.EmailAddress, StringComparison.InvariantCultureIgnoreCase));
            return MapMayBeNull(user, _mapper);
        }
    }
}