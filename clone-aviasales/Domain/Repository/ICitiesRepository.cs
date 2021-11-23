using clone_aviasales.Domain.Model;
using System.Collections.Generic;

namespace clone_aviasales.Domain.Repository
{
    public interface ICitiesRepository
    {
        public IDictionary<string, City> FindCities(IEnumerable<FindCitiesParams> citiesParams);
    }
}
