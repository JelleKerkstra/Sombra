using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class
        ApproveCharityActionRequestHandler : AsyncCrudRequestHandler<ApproveCharityActionRequest,
            ApproveCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public ApproveCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public override async Task<ApproveCharityActionResponse> Handle(ApproveCharityActionRequest message)
        {
            var charityAction =
                await _context.CharityActions.FirstOrDefaultAsync(b =>
                    b.CharityActionKey.Equals(message.CharityActionKey));
            if (charityAction != null)
            {
                if (charityAction.IsApproved)
                    return Error(ErrorType.AlreadyActive);

                charityAction.IsApproved = true;

                if (await _context.TrySaveChangesAsync())
                {
                    await _bus.PublishAsync(_mapper.Map<CharityActionCreatedEvent>(charityAction));
                    return Success();
                }

                return Error(ErrorType.DatabaseError);
            }

            return Error(ErrorType.NotFound);
        }
    }
}