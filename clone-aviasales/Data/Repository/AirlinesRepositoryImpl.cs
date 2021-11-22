using clone_aviasales.Data.Source;
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
        //todo: change ienuramble<string> to ienurable<citiesforfindparams>
        public IDictionary<string, Airline> FindAirlines(ISet<string> airlinesForFind)
        {
            return cacheDataSource.FindAirlines(airlinesForFind);
        }
    }
}
