using clone_aviasales.Data.Source;
using clone_aviasales.Domain.Model;
using clone_aviasales.Domain.Repository;
using System.Collections.Generic;

namespace clone_aviasales.Data.Repository
{
    public class CitiesRepositoryImpl : ICitiesRepository
    {
        private readonly CitiesCacheDataSource cacheDataSource;

        public CitiesRepositoryImpl(CitiesCacheDataSource cacheDataSource)
        {
            this.cacheDataSource = cacheDataSource;
        }

        public IDictionary<string, City> FindCities(IEnumerable<FindCitiesParams> citiesParams)
        {
            return cacheDataSource.FindCities(citiesParams);
        }
    }
}
