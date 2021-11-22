using System.Collections.Generic;

namespace clone_aviasales.Data.Repository
{
    public interface ICitiesRepository
    {
        public IDictionary<string, City> FindCities(ISet<string> citiesForFind);
    }
}
