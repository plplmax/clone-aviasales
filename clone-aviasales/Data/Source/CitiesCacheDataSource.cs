using clone_aviasales.Domain.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public IDictionary<string, City> FindCities(IEnumerable<FindCitiesParams> citiesParams)
        {
            var citiesArray = cities.RootElement.EnumerateArray();
            IDictionary<string, City> result = new Dictionary<string, City>(citiesParams.Count());

            foreach (var cityItem in citiesArray)
            {
                string code = cityItem.GetProperty("code").GetString();
                if (citiesParams.FirstOrDefault(city => city.IataCode == code) != null) result.Add(code, new City() { Name = cityItem.GetProperty("name_translations").GetProperty("ru").GetString(), Timezone = cityItem.GetProperty("time_zone").GetString() });
                if (result.Count == citiesParams.Count()) break;
            }

            return result;
        }


    }
}
