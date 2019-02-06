﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class GetStoryByKeyRequestHandler : AsyncRequestHandler<GetStoryByKeyRequest, GetStoryByKeyResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetStoryByKeyRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<GetStoryByKeyResponse> Handle(GetStoryByKeyRequest message)
        {
            var story = await _context.Stories.IncludeAll().FirstOrDefaultAsync(b => b.StoryKey.Equals(message.StoryKey));

            return MapMayBeNull(story, _mapper);
        }
    }
}