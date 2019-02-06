﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponder
    {
        protected readonly IBus Bus;
        public IAutoResponderRequestDispatcher AutoResponderMessageDispatcher { get; }

        public AutoResponder(IBus bus, IAutoResponderRequestDispatcher messageDispatcher)
        {
            Bus = bus;
            AutoResponderMessageDispatcher = messageDispatcher;
        }

        public virtual void RespondAsync(Assembly assembly)
        {
            RespondAsync(assembly.GetTypes().ToArray());
        }

        public virtual void RespondAsync(params Type[] responderTypes)
        {
            var genericBusRepondMethod = GetRespondMethodOfBus(nameof(Bus.RespondAsync), typeof(Func<,>));
            var responderInfos = GetResponderInfos(responderTypes, typeof(IAsyncRequestHandler<,>));
            Type ResponderTypeFromRequestTypeDelegate(Type messageType, Type responseType) => typeof(Func<,>).MakeGenericType(messageType, typeof(Task<>).MakeGenericType(responseType));
            InvokeMethods(responderInfos, nameof(AutoResponderMessageDispatcher.DispatchAsync), genericBusRepondMethod, ResponderTypeFromRequestTypeDelegate);
        }

        protected virtual void InvokeMethods(IEnumerable<KeyValuePair<Type, AutoResponderRequestHandlerInfo[]>> responderInfos, string dispatchName, MethodInfo genericBusRepondMethod, Func<Type, Type, Type> responderTypeFromRequestTypeDelegate)
        {
            foreach (var kv in responderInfos)
            {
                foreach (var responderinfo in kv.Value)
                {
                    var dispatchMethod =
                        AutoResponderMessageDispatcher.GetType()
                            .GetMethod(dispatchName, BindingFlags.Instance | BindingFlags.Public)
                            .MakeGenericMethod(responderinfo.MessageType, responderinfo.ResponseType, responderinfo.ConcreteType);

                    var dispatchDelegate = Delegate.CreateDelegate(responderTypeFromRequestTypeDelegate(responderinfo.MessageType, responderinfo.ResponseType), AutoResponderMessageDispatcher, dispatchMethod);

                    var busRespondMethod = genericBusRepondMethod.MakeGenericMethod(responderinfo.MessageType, responderinfo.ResponseType);
                    busRespondMethod.Invoke(Bus, new object[] { dispatchDelegate });
                }
            }
        }

        protected virtual MethodInfo GetRespondMethodOfBus(string methodName, Type parmType)
        {
            return typeof(IBus).GetMethods()
                .Where(m => m.Name == methodName)
                .Select(m => new { Method = m, Params = m.GetParameters() })
                .Single(m => m.Params.Length == 1
                             && m.Params[0].ParameterType.GetGenericTypeDefinition() == parmType
                ).Method;
        }

        protected virtual IEnumerable<KeyValuePair<Type, AutoResponderRequestHandlerInfo[]>> GetResponderInfos(IEnumerable<Type> types, Type interfaceType)
        {
            foreach (var concreteType in types.Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract))
            {
                var subscriptionInfos = concreteType.GetInterfaces()
                    .Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == interfaceType && !i.GetGenericArguments()[0].IsGenericParameter && !i.GetGenericArguments()[1].IsGenericParameter)
                    .Select(i => new AutoResponderRequestHandlerInfo(concreteType, i, i.GetGenericArguments()[0], i.GetGenericArguments()[1]))
                    .ToArray();

                if (subscriptionInfos.Any())
                    yield return new KeyValuePair<Type, AutoResponderRequestHandlerInfo[]>(concreteType, subscriptionInfos);
            }
        }
    }
}