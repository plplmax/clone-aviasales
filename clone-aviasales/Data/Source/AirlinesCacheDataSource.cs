using clone_aviasales.Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace clone_aviasales.Data.Source
{
    public class AirlinesCacheDataSource : CacheDataSource
    {
        private const string AIRLINES_KEY = "Airlines";

        public AirlinesCacheDataSource(IConfiguration configuration) : base(configuration[AIRLINES_KEY]) { }
    }
}
