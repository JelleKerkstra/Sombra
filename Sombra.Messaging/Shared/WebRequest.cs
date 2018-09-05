using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Shared
{
    public class WebRequest : Message
    {
        public WebRequest() { }

        public WebRequest(string url, DateTime dateTimeStamp, Dictionary<string, string> routeValues) { }

        public string Url { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public Dictionary<string, string> RouteValues { get; set; }
    }
}