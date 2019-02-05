using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByKeyRequestHandler : AsyncRequestHandler<GetCharityActionByKeyRequest, GetCharityActionByKeyResponse>
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionByKeyRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public override async Task<GetCharityActionByKeyResponse> Handle(GetCharityActionByKeyRequest message)
        {
            var charityAction = await _charityActionContext.CharityActions.Include(b => b.UserKeys).Include(b => b.Charity).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));

            return MapMayBeNull(charityAction, _mapper);
        }
    }
}