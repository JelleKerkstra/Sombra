using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriber : AutoSubscriber
    {
        public CustomAutoSubscriber(IBus bus, IAutoSubscriberMessageDispatcher messageDispatcher, string subscriptionIdPrefix) : base(bus, subscriptionIdPrefix)
        {
            AutoSubscriberMessageDispatcher = messageDispatcher;
        }

        public override void SubscribeAsync(params Type[] consumerTypes)
        {
            var genericBusSubscribeMethod = GetSubscribeMethodOfBus(nameof(IBus.SubscribeAsync), typeof(Func<,>));
            var subscriptionInfos = GetSubscriptionInfos(consumerTypes, typeof(IAsyncEventHandler<>)).ToList();
            Type SubscriberTypeFromMessageTypeDelegate(Type messageType) => typeof(Func<,>).MakeGenericType(messageType, typeof(Task));
            InvokeMethods(subscriptionInfos, DispatchAsyncMethodName, genericBusSubscribeMethod, SubscriberTypeFromMessageTypeDelegate);
        }

        protected override IEnumerable<KeyValuePair<Type, AutoSubscriberConsumerInfo[]>> GetSubscriptionInfos(IEnumerable<Type> types, Type interfaceType)
        {
            foreach (var concreteType in types.Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract))
            {
                var subscriptionInfos = concreteType.GetInterfaces()
                    .Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == interfaceType && !i.GetGenericArguments()[0].IsGenericParameter)
                    .Select(i => new AutoSubscriberConsumerInfo(concreteType, i, i.GetGenericArguments()[0]))
                    .ToArray();

                if (subscriptionInfos.Any())
                    yield return new KeyValuePair<Type, AutoSubscriberConsumerInfo[]>(concreteType, subscriptionInfos);
            }
        }
    }
}