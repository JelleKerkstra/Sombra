using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class Content : Entity
    {
        public System.Guid Key { get; set; }
        public string Name { get; set; }
        public Core.Enums.SearchContentType Type { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}