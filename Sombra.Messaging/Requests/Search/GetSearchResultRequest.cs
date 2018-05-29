using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Search;

namespace Sombra.Messaging.Requests.Search
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetSearchResultRequest : Request<GetSearchResultResponse>
    {
        [CacheKey]
        public List<string> Keywords { get; set; }

        [CacheKey]
        public SortOrder SortOrder { get; set; }

        [CacheKey]
        public SearchContentType SearchContentType { get; set; }

        [CacheKey]
        public Category Categories { get; set; }

        [CacheKey]
        public int PageSize{ get; set; }

        [CacheKey]
        public int PageNumber{ get; set; }
    }
}