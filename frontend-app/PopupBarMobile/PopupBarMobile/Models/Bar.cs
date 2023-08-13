using Newtonsoft.Json;

namespace PopupBarMobile.Models
{
    public class Bar
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("menuItems")]
        public List<BarMenuItem> barMenuItems { get; set; }
    }
}