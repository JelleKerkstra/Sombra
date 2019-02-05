using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class GetCharityByKeyRequestHandler : AsyncRequestHandler<GetCharityByKeyRequest, GetCharityByKeyResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;

        public GetCharityByKeyRequestHandler(CharityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<GetCharityByKeyResponse> Handle(GetCharityByKeyRequest message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(b => b.CharityKey.Equals(message.CharityKey));
            return MapMayBeNull(charity, _mapper);
        }
    }
}