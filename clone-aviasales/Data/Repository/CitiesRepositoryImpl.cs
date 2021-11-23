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
        //todo: change ienuramble<string> to ienurable<citiesforfindparams>
        public IDictionary<string, City> FindCities(ISet<string> citiesForFind)
        {
            return cacheDataSource.FindCities(citiesForFind);
        }
    }
}
