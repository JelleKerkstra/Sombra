using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Infrastructure;
using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class CreateCharityActionRequestHandler : AsyncCrudRequestHandler<CreateCharityActionRequest, CreateCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;

        public CreateCharityActionRequestHandler(CharityActionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<CreateCharityActionResponse> Handle(CreateCharityActionRequest message)
        {
            var charityAction = _mapper.Map<CharityAction>(message);
            if (charityAction.CharityActionKey == default)
                return Error(ErrorType.InvalidKey);

            var charity = await _context.Charities.FirstOrDefaultAsync(c => c.CharityKey == message.CharityKey);
            if (charity == null)
                return Error(ErrorType.CharityNotFound);

            charityAction.Charity = charity;
            _context.CharityActions.Add(charityAction);

            return await _context.TrySaveChangesAsync<CreateCharityActionResponse>();
        }
    }
}
