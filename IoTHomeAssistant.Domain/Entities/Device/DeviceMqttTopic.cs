using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceMqttTopic : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public MqttTopicTypeEnum TopicType { get; set; }

        public virtual Device Device { get; set; }
        public virtual MqttBroker MqttBroker { get; set; }
    }
}
