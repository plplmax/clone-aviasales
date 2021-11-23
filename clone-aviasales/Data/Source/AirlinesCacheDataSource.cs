using clone_aviasales.Domain.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace clone_aviasales.Data.Source
{
    public class AirlinesCacheDataSource
    {
        private const string FILENAME = "airlines.json";
        private static JsonDocument airlines;

        public AirlinesCacheDataSource()
        {
            InitializeAirlines();
        }

        private static async void InitializeAirlines()
        {
            string allAirlines = await File.ReadAllTextAsync(FILENAME);
            airlines = JsonDocument.Parse(allAirlines);
        }

        public IDictionary<string, Airline> FindAirlines(IEnumerable<FindAirlinesParams> airlinesParams)
        {
            var airlinesArray = airlines.RootElement.EnumerateArray();
            IDictionary<string, Airline> result = new Dictionary<string, Airline>(airlinesParams.Count());

            foreach (var airline in airlinesArray)
            {
                string iataCode = airline.GetProperty("code").GetString();
                if (airlinesParams.FirstOrDefault(airline => airline.IataCode == iataCode) != null) result.Add(iataCode, new Airline() { Name = airline.GetProperty("name_translations").GetProperty("en").GetString() });
                if (result.Count == airlinesParams.Count()) break;
            }

            return result;
        }
    }
}
