using clone_aviasales.Domain.Model;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace clone_aviasales.Data.Source
{
    class TicketsCloudDataSource
    {
        private static readonly HttpClient httpClient = new();
        private const string ENDPOINT = "prices_for_dates";
        private readonly string token;
        private readonly string baseUrl;

        public TicketsCloudDataSource(IConfiguration configuration)
        {
            token = configuration["Token"];
            baseUrl = configuration["BaseUrl"];
        }

        public Task<string> FetchTickets(TicketRequest request)
        {
            string requestJson = JsonSerializer.Serialize(request);
            IDictionary<string, string> requestDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(requestJson);
            requestDictionary.Add("token", token);
            string uri = QueryHelpers.AddQueryString($"{baseUrl}{ENDPOINT}", requestDictionary);
            return httpClient.GetStringAsync(uri);
        }
    }
}
