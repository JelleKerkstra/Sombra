﻿using System;

namespace Sombra.Messaging.Events
{
    public class UserCreatedEvent : Event
    {
        public Guid UserKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public DateTime UserCreated { get; set; }
        public string ProfileImage { get; set; }
    }
}