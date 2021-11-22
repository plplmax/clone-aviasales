using System.Text.Json.Serialization;

namespace clone_aviasales.Data
{
    public class Airline
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
