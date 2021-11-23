using clone_aviasales.Domain.Model;
using System.Collections.Generic;

namespace clone_aviasales.Domain.Repository
{
    public interface IAirlinesRepository
    {
        public IDictionary<string, Airline> FindAirlines(ISet<string> airlinesForFind);
    }
}
