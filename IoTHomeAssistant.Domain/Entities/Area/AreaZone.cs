namespace IoTHomeAssistant.Domain.Entities
{
    public class AreaZone : IEntity<int> 
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Title { get; set; }

        public virtual Area Area { get; set; }
    }
}