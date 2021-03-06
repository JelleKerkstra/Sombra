﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService
{
    public class MakeDonationRequestHandler : AsyncCrudRequestHandler<MakeDonationRequest, MakeDonationResponse>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public MakeDonationRequestHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<MakeDonationResponse> Handle(MakeDonationRequest message)
        {
            if (message.CharityActionKey.HasValue)
            {
                var charityAction = await _context.CharityActions.SingleOrDefaultAsync(c => c.CharityActionKey == message.CharityActionKey.Value);
                if (charityAction == null)
                    return Error(ErrorType.CharityActionNotFound);

                var charityActionDonation = _mapper.Map<CharityActionDonation>(message);
                charityActionDonation.CharityAction = charityAction;

                if (message.UserKey.HasValue && !message.IsAnonymous)
                {
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey.Value);
                    if (user == null)
                        return Error(ErrorType.UserNotFound);

                    charityActionDonation.User = user;
                }

                if (!message.UserKey.HasValue && !message.IsAnonymous)
                {
                    var user = _mapper.Map<User>(message);
                    user.UserKey = Guid.NewGuid();
                    _context.Users.Add(user);
                    charityActionDonation.User = user;
                }

                _context.CharityActionDonations.Add(charityActionDonation);
                return await _context.TrySaveChangesAsync<MakeDonationResponse>(response =>
                {
                    response.CoverImage = charityAction.CoverImage;
                    response.ThankYou = charityAction.ThankYou;
                });
            }


            var charity = await _context.Charities.SingleOrDefaultAsync(c => c.CharityKey == message.CharityKey);
            if (charity == null)
                return Error(ErrorType.CharityNotFound);

            var charityDonation = _mapper.Map<CharityDonation>(message);
            charityDonation.Charity = charity;

            if (message.UserKey.HasValue && !message.IsAnonymous)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey.Value);
                if (user == null)
                    return Error(ErrorType.UserNotFound);

                charityDonation.User = user;
            }

            if (!message.UserKey.HasValue && !message.IsAnonymous)
            {
                var user = _mapper.Map<User>(message);
                user.UserKey = Guid.NewGuid();
                _context.Users.Add(user);
                charityDonation.User = user;
            }

            _context.CharityDonations.Add(charityDonation);
            return await _context.TrySaveChangesAsync<MakeDonationResponse>(response =>
            {
                response.CoverImage = charity.CoverImage;
                response.ThankYou = charity.ThankYou;
            });
        }
    }
}