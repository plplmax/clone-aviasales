using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace clone_aviasales.Domain.Model
{
    public class TicketsFilters
    {
        [JsonPropertyName("transfers_count")]
        [BindProperty(Name = "transfers_count", SupportsGet = true)]
        public IEnumerable<byte> Transfers { get; set; }
        [JsonPropertyName("airlines")]
        [BindProperty(Name = "airlines", SupportsGet = true)]
        public IEnumerable<string> Airlines { get; set; }
        [JsonPropertyName("duration")]
        [BindProperty(Name = "duration", SupportsGet = true)]
        public short DurationInHours { get; set; }
    }
}
