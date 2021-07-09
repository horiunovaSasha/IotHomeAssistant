namespace IoTHomeAssistant.Web.Models
{
    public class LightRequest
    {
        public int Id { get; set; }
        public bool Toggle { get; set; }
        public int Brightness { get; set; }
        public string Color { get; set; }
    }
}
