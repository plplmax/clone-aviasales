using clone_aviasales.Domain.Core;
using clone_aviasales.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace clone_aviasales.Data.Source
{
    public abstract class CacheDataSource
    {
        private readonly string filename;
        private readonly JsonDocument cache;
        private readonly object _lockCache = new();

        protected CacheDataSource(string filename)
        {
            this.filename = filename;
            cache = FetchCache();
        }

        private JsonDocument FetchCache()
        {
            lock (_lockCache)
            {
                string json = File.ReadAllText(filename);
                return JsonDocument.Parse(json);
            }
        }

        public IDictionary<string, T> Find<T>(IEnumerable<FindParams> parameters, Func<JsonElement, T> mapper)
        {
            JsonElement.ArrayEnumerator itemsArray = new();
            if (_lockCache != null) itemsArray = cache.RootElement.EnumerateArray();
            IDictionary<string, T> result = new Dictionary<string, T>(parameters.Count());

            foreach (var item in itemsArray)
            {
                string iataCode = item.GetIataCode();
                if (parameters.FirstOrDefault(paramsItem => paramsItem.IataCode == iataCode) != null)
                {
                    result.Add(iataCode, mapper(item));
                }
                if (result.Count == parameters.Count()) break;
            }

            return result;
        }
    }
}
