﻿using Sombra.Core.Enums;
using System;
using System.Collections;

namespace Sombra.Messaging.Events
{
    public class CreateCharityActionEvent
    {
        // TODO at more data relevant for charityAction
        public Guid CharityActionkey { get; set; }
        public Guid Charitykey { get; set; }
        public ICollection UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Discription { get; set; }
        public string CoverImage { get; set; }
    }
}
