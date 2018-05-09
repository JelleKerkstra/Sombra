﻿using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityService.DAL
{
    public class CharityEntity : Entity
    {
        // TODO at more data relevant for charity
        public string CharityId { get; set; }    
        public string NameOwner { get; set; }
        public string NameCharity { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public string KVKNumber { get; set; }
        public string IBAN { get; set; }
    }
}
