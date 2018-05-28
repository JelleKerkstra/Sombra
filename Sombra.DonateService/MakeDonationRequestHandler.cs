﻿using System;
using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.DonateService
{
    public class MakeDonationRequestHandler : IAsyncRequestHandler<MakeDonationRequest, MakeDonationResponse>
    {
        private readonly DonationsContext _context;

        public MakeDonationRequestHandler(DonationsContext context)
        {
            _context = context;
        }

        public Task<MakeDonationResponse> Handle(MakeDonationRequest message)
        {
            throw new NotImplementedException();
        }
    }
}