using clone_aviasales.Data.Source;
using clone_aviasales.Domain.Model;
using clone_aviasales.Domain.Repository;
using System.Collections.Generic;

namespace clone_aviasales.Data.Repository
{
    public class AirlinesRepositoryImpl : IAirlinesRepository
    {
        private readonly AirlinesCacheDataSource cacheDataSource;

        public AirlinesRepositoryImpl(AirlinesCacheDataSource cacheDataSource)
        {
            this.cacheDataSource = cacheDataSource;
        }

        public IDictionary<string, Airline> FindAirlines(IEnumerable<FindAirlinesParams> airlinesParams)
        {
            return cacheDataSource.FindAirlines(airlinesParams);
        }
    }
}
