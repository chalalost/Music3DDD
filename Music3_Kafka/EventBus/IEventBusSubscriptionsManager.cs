using Music3_Kafka.EventBus.Abstractions;
using Music3_Kafka.EventBus.Events;
using System;
using System.Collections.Generic;
using static Music3_Kafka.InMemoryEventBusSubscriptionsManager;

namespace Music3_Kafka.EventBus
{
    /// <summary>
    /// danh sách các model mà service sẽ nhận để xử lý, tránh trường hợp xử lý hết message mà kafka bắn về
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIntegrationEventHandler;

        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
