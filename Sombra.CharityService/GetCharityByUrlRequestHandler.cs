﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class GetCharityByUrlRequestHandler : AsyncRequestHandler<GetCharityByUrlRequest, GetCharityByUrlResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharityByUrlRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<GetCharityByUrlResponse> Handle(GetCharityByUrlRequest message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.Url.Equals(message.Url, StringComparison.OrdinalIgnoreCase) && b.IsApproved);
            return MapMayBeNull(charity, _mapper);
        }
    }
}