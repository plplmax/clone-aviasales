using clone_aviasales.Domain.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace clone_aviasales.Data.Source
{
    public class CitiesCacheDataSource
    {
        private const string FILENAME = "cities.json";
        private static JsonDocument cities;

        public CitiesCacheDataSource()
        {
            InitializeCities();
        }

        private static async void InitializeCities()
        {
            string allCities = await File.ReadAllTextAsync(FILENAME);
            cities = JsonDocument.Parse(allCities);
        }

        public IDictionary<string, City> FindCities(ISet<string> citiesForFind)
        {
            var test = cities.RootElement.EnumerateArray();
            IDictionary<string, City> temp = new Dictionary<string, City>(citiesForFind.Count);

            foreach (var t in test)
            {
                string code = t.GetProperty("code").GetString();
                if (citiesForFind.Contains(code)) temp.Add(code, new City() { Name = t.GetProperty("name_translations").GetProperty("ru").GetString(), Timezone = t.GetProperty("time_zone").GetString() });
                if (temp.Count == citiesForFind.Count) break;
            }

            return temp;
        }


    }
}
