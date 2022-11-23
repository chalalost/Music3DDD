using Music3_Kafka.EventBus.Events;
using System.Threading.Tasks;

namespace Music3_Kafka.EventBus.Abstractions
{
    //trình xử lý sự kiện tích hợp (hoặc phương thức gọi lại),
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
