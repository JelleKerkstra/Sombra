using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UpdateUserRequestHandler : AsyncCrudRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateUserRequestHandler(UserContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public override async Task<UpdateUserResponse> Handle(UpdateUserRequest message)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (existingUser == null)
                return Error(ErrorType.NotFound);

            var isEmailAddressUniqueHandler = new UserEmailExistsRequestHandler(_context);
            var request = new UserEmailExistsRequest
            {
                CurrentUserKey = message.UserKey,
                EmailAddress = message.EmailAddress
            };

            var response = await isEmailAddressUniqueHandler.Handle(request);
            if (response.EmailExists)
                return Error(ErrorType.EmailExists);

            _context.Entry(existingUser).CurrentValues.SetValues(message);
            if (!await _context.TrySaveChangesAsync()) return new UpdateUserResponse();

            var userUpdatedEvent = _mapper.Map<UserUpdatedEvent>(existingUser);
            await _bus.PublishAsync(userUpdatedEvent);

            return Success();
        }
    }
}