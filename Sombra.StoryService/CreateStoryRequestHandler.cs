using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CreateStoryRequestHandler : AsyncCrudRequestHandler<CreateStoryRequest, CreateStoryResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public CreateStoryRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<CreateStoryResponse> Handle(CreateStoryRequest message)
        {
            var story = _mapper.Map<Story>(message);
            if (story.StoryKey == default)
                return Error(ErrorType.InvalidKey);

            if (message.AuthorUserKey.HasValue)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.AuthorUserKey);
                if (user != null)
                {
                    story.Author = user;
                }
                else
                {
                    return Error(ErrorType.InvalidUserKey);
                }
            }

            var charity = await _context.Charities.FirstOrDefaultAsync(c => c.CharityKey == message.CharityKey);
            if (charity == null)
                return Error(ErrorType.CharityNotFound);

            story.Charity = charity;
            _context.Stories.Add(story);

            return await _context.TrySaveChangesAsync<CreateStoryResponse>();
        }
    }
}