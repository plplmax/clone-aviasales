using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace clone_aviasales.Domain.Model
{
    public class TicketsResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public List<DataResponse> Data { get; set; } = new();
        [JsonPropertyName("cities")]
        public IDictionary<string, City> Cities { get; set; }
        [JsonPropertyName("airlines")]
        public IDictionary<string, Airline> Airlines { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
