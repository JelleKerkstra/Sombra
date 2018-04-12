using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class CredentialType : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
    }
}