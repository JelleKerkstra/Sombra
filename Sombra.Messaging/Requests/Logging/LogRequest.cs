﻿using System;
using System.Collections.Generic;
using Sombra.Messaging.Responses.Logging;

namespace Sombra.Messaging.Requests.Logging
{
    public class LogRequest : Request<LogResponse>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<string> MessageTypes { get; set; }
    }
}