using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class UpdateCharityRequestHandler : AsyncCrudRequestHandler<UpdateCharityRequest, UpdateCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateCharityRequestHandler(CharityContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public override async Task<UpdateCharityResponse> Handle(UpdateCharityRequest message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(u => u.CharityKey == message.CharityKey);
            if (charity == null)
                return Error(ErrorType.NotFound);

            _context.Entry(charity).CurrentValues.SetValues(message);

            return await _context.TrySaveChangesAsync<UpdateCharityResponse>(async () =>
            {
                await _bus.PublishAsync<Charity, CharityUpdatedEvent>(charity, _mapper);
            });
        }
    }
}