using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class ApproveCharityRequestHandler : AsyncCrudRequestHandler<ApproveCharityRequest, ApproveCharityResponse>
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

        public override async Task<ApproveCharityResponse> Handle(ApproveCharityRequest message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.CharityKey.Equals(message.CharityKey));
            if (charity != null)
            {
                if (charity.IsApproved)
                    return Error(ErrorType.AlreadyActive);

                charity.IsApproved = true;

                if (await _context.TrySaveChangesAsync())
                {
                    await _bus.PublishAsync(_mapper.Map<CharityCreatedEvent>(charity));
                    return Success();
                }

                return Error(ErrorType.DatabaseError);
            }

            return Error(ErrorType.NotFound);
        }
    }
}