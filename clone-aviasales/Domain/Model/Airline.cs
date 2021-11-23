using System.Text.Json.Serialization;

namespace clone_aviasales.Domain.Model
{
    public class Airline
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
