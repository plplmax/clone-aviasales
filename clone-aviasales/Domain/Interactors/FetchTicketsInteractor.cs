using clone_aviasales.Data;
using clone_aviasales.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clone_aviasales.Domain.Interactors
{
    public class FetchTicketsInteractor
    {
        private readonly ITicketsRepository ticketsRepository;
        private readonly ICitiesRepository citiesRepository;
        private readonly IAirlinesRepository airlinesRepository;

        public FetchTicketsInteractor(ITicketsRepository ticketsRepository, ICitiesRepository citiesRepository, IAirlinesRepository airlinesRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.citiesRepository = citiesRepository;
            this.airlinesRepository = airlinesRepository;
        }

        public async Task<TicketsResponse> Execute(TicketRequest request)
        {
            TicketsResponse responseWithoutCities = await ticketsRepository.FetchTickets(request);
            IEnumerable<string> cities = responseWithoutCities.Data.SelectMany(ticket => new string[] { ticket.Origin, ticket.Destination });
            ISet<string> citiesForFind = new HashSet<string>(cities);
            IDictionary<string, City> findedCities = citiesRepository.FindCities(citiesForFind);
            responseWithoutCities.Cities = findedCities;
            IEnumerable<string> airlines = responseWithoutCities.Data.Select(ticket => ticket.Airline);
            ISet<string> airlinesForFind = new HashSet<string>(airlines);
            IDictionary<string, Airline> findedAirlines = airlinesRepository.FindAirlines(airlinesForFind);
            responseWithoutCities.Airlines = findedAirlines;
            return responseWithoutCities;
        }
    }
}
