using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace clone_aviasales.Domain.Model
{
    [BindRequired]
    public class TicketRequest
    {
        [JsonPropertyName("origin")]
        [BindProperty(Name = "origin_iata", SupportsGet = true)]
        public string Origin { get; set; }
        [JsonPropertyName("destination")]
        [BindProperty(Name = "destination_iata", SupportsGet = true)]
        public string Destination { get; set; }
        [JsonPropertyName("departure_at")]
        [BindProperty(Name = "depart_date", SupportsGet = true)]
        public string DepartureAt { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        [JsonPropertyName("return_at")]
        [BindProperty(Name = "return_date", SupportsGet = true)]
        public string ReturnAt { get; set; }
        [JsonPropertyName("currency")]
        [BindProperty(Name = "currency", SupportsGet = true)]
        public string Currency { get; set; }
        [JsonIgnore]
        [BindingBehavior(BindingBehavior.Optional)]
        [BindProperty(Name = "filters", SupportsGet = true)]
        public TicketsFilters Filters { get; set; }
    }
}
