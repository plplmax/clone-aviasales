using clone_aviasales.Domain.Model;
using System.Collections.Generic;
using System.IO;
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

        public IDictionary<string, Airline> FindAirlines(ISet<string> airlinesForFind)
        {
            var test = airlines.RootElement.EnumerateArray();
            IDictionary<string, Airline> temp = new Dictionary<string, Airline>(airlinesForFind.Count);

            foreach (var t in test)
            {
                string code = t.GetProperty("code").GetString();
                if (airlinesForFind.Contains(code)) temp.Add(code, new Airline() { Name = t.GetProperty("name_translations").GetProperty("en").GetString() });
                if (temp.Count == airlinesForFind.Count) break;
            }

            return temp;
        }
    }
}
