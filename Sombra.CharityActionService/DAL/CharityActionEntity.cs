﻿using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using System;
using System.Collections.Generic;

namespace Sombra.CharityActionService.DAL
{
    public class CharityActionEntity : Entity
    {
        // TODO at more data relevant for charityAction
        public Guid CharityActionkey { get; set; }
        public Guid Charitykey { get; set; }    
        public virtual ICollection<UserKey> UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Discription { get; set; }
        public string CoverImage { get; set; }

    }
}
