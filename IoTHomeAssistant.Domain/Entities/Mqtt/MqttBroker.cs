namespace IoTHomeAssistant.Domain.Entities
{
    public class MqttBroker : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public bool UseCredentials { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
