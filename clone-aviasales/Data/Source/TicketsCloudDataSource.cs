using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace clone_aviasales.Data.Source
{
    class TicketsCloudDataSource
    {
        private const string TOKEN = "1d355f6e877f646f057822e7947a7827";
        private const string BASE_URI = "https://api.travelpayouts.com/aviasales/v3/prices_for_dates";
        private static readonly HttpClient httpClient = new();

        public Task<string> FetchTickets(TicketRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            IDictionary<string, string> requestDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(requestJson);
            requestDictionary.Add("token", TOKEN);
            string uri = QueryHelpers.AddQueryString(BASE_URI, requestDictionary);
            return httpClient.GetStringAsync(uri);
        }
    }
}
