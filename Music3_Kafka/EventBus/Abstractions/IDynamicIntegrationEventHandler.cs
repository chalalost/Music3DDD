using System.Threading.Tasks;

namespace Music3_Kafka.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
