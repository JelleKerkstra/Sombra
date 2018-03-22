using System.Collections.Generic;
using Sombra.Core;

namespace Sombra.IdentityService
{
  public class Role : Entity
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
  }
}