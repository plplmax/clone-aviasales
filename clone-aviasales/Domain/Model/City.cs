using System.Text.Json.Serialization;

namespace clone_aviasales.Domain.Model
{
    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }
}
