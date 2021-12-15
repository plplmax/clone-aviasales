using clone_aviasales.Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace clone_aviasales.Data.Source
{
    public class CitiesCacheDataSource : CacheDataSource
    {
        private const string CITIES_KEY = "Cities";

        public CitiesCacheDataSource(IConfiguration configuration) : base(configuration[CITIES_KEY]) { }
    }
}
