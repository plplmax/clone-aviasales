using clone_aviasales.Domain.Model;
using clone_aviasales.Domain.Repository;
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
            TicketsResponse response = await ticketsRepository.FetchTickets(request);
            IEnumerable<FindParams> iataCityCodes = response.Data
                .SelectMany(ticket => new string[] { ticket.Origin, ticket.Destination })
                .ToHashSet()
                .Select(iataCityCode => new FindParams() { IataCode = iataCityCode });
            response.Cities = citiesRepository.FindCities(iataCityCodes);
            IEnumerable<FindParams> iataAirlinesCodes = response.Data
                .Select(ticket => ticket.Airline)
                .ToHashSet()
                .Select(iataAirlineCode => new FindParams() { IataCode = iataAirlineCode });
            response.Airlines = airlinesRepository.FindAirlines(iataAirlinesCodes);
            return response;
        }
    }
}
