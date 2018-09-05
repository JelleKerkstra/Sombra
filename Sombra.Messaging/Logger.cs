﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging
{
    public static class Logger
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private static IBus Bus => _serviceProvider.GetRequiredService<IBus>();

        public static Task LogMessageAsync(IMessage message)
            => Bus.SendAsync(ServiceInstaller.LoggingQueue, message);

        public static Task LogExceptionAsync(Exception exception, bool isHandled)
            => LogExceptionAsync(exception, isHandled, null);

        public static Task LogExceptionAsync(Exception exception, bool isHandled, string handlerName)
            => Bus.SendAsync(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, isHandled, handlerName));

        public static void LogException(Exception exception, bool isHandled)
            => LogException(exception, isHandled, null);

        public static void LogException(Exception exception, bool isHandled, string handlerName)
            => Bus.Send(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, isHandled, handlerName));

        public static void LogWebrequest(WebRequest request)
            => Bus.Send(ServiceInstaller.WebRequestsQueue, request);

        public static void LogWebrequest(string url, DateTime dateTimeStamp, Dictionary<string, string> routeValues)
            => LogWebrequest(new WebRequest(url, dateTimeStamp, routeValues));
    }
}