﻿using System;
using AutoMapper;
using Newtonsoft.Json;

namespace Sombra.Messaging
{
    [Serializable]
    public abstract class Message : IMessage
    {
        protected Message()
        {
            MessageCreated = DateTime.UtcNow;
            MessageType = GetType().FullName;
        }

        [IgnoreMap]
        public DateTime MessageCreated { get; }

        [CacheKey]
        [IgnoreMap]
        public string MessageType { get; }

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}