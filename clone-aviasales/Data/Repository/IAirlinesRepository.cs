using System.Collections.Generic;

namespace clone_aviasales.Data.Repository
{
    public interface IAirlinesRepository
    {
        public IDictionary<string, Airline> FindAirlines(ISet<string> airlinesForFind);
    }
}
