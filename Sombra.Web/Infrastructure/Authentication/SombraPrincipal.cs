﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Sombra.Core.Enums;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class SombraPrincipal : ClaimsPrincipal
    {
        public SombraPrincipal(SombraIdentity identity) : base(identity)
        {
            Identity = identity;
        }

        public new SombraIdentity Identity { get; }
        public Guid Key => Identity.UserKey;
        public Role Role => Identity.Role;

        public bool IsInRole(Role role) => Role.HasFlag(role);

        public bool IsDonator => IsInRole(Role.Donator);
        public bool IsCharityOwner => IsInRole(Role.CharityOwner);
        public bool IsCharityUser => IsInRole(Role.CharityUser);
        public bool IsEventOrganiser => IsInRole(Role.EventOrganiser);
        public bool IsEventParticipant => IsInRole(Role.EventParticipant);
    }
}