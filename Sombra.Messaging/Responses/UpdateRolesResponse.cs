﻿using System.Collections.Generic;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public class UpdateRolesResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
        public List<Role> Roles { get; set; }
    }
}