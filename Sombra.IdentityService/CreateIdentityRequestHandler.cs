﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class CreateIdentityRequestHandler : IAsyncRequestHandler<CreateIdentityRequest, CreateIdentityResponse>
    {
        private readonly AuthenticationContext _context;

        public CreateIdentityRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<CreateIdentityResponse> Handle(CreateIdentityRequest message)
        {
            if (message.UserKey == default) return new CreateIdentityResponse
            {
                ErrorType = ErrorType.InvalidKey
            };

            if (message.CredentialType == CredentialType.Email)
            {
                if (await _context.Credentials.AnyAsync(c => c.CredentialType == CredentialType.Email && c.Identifier.Equals(message.Identifier, StringComparison.OrdinalIgnoreCase)))
                {
                    return new CreateIdentityResponse
                    {
                        ErrorType = ErrorType.EmailExists
                    };
                }
            }

            var activationToken = Hash.SHA256(Guid.NewGuid().ToString());

            var user = new User
            {
                ActivationToken = activationToken,
                Name = message.UserName,
                UserKey = message.UserKey,
                ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1),
                Role = message.Role
            };

            var credential = new Credential
            {
                Identifier = message.Identifier,
                Secret = message.Secret,
                CredentialType = message.CredentialType,
                User = user
            };

            _context.Users.Add(user);
            _context.Credentials.Add(credential);

            return await _context.TrySaveChangesAsync<CreateIdentityResponse>(response =>
            {
                response.ActivationToken = activationToken;
                return response;
            });
        }
    }
}