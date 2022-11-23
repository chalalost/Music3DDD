using System;

namespace Music3_Kafka.EventBus.Events
{
    //sự kiện tích hợp để đăng ký
    public class IntegrationEvent
    {

        //  [JsonInclude]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // [JsonInclude]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
