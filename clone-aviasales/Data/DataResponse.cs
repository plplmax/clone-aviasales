using System.Text.Json.Serialization;

namespace clone_aviasales.Data
{
    public class DataResponse
    {
        [JsonPropertyName("origin")]
        public string Origin { get; set; }
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
        [JsonPropertyName("departure_at")]
        public string DepartureAt { get; set; }
        [JsonPropertyName("return_at")]
        public string ReturnAt { get; set; }
        [JsonPropertyName("origin_airport")]
        public string OriginAirport { get; set; }
        [JsonPropertyName("destination_airport")]
        public string DestinationAirport { get; set; }
        [JsonPropertyName("airline")]
        public string Airline { get; set; }
        [JsonPropertyName("price")]
        public short Price { get; set; }
        [JsonPropertyName("transfers")]
        public byte Transfers { get; set; }
        [JsonPropertyName("return_transfers")]
        public byte ReturnTransfers { get; set; }
        [JsonPropertyName("duration")]
        public short Duration { get; set; }
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
