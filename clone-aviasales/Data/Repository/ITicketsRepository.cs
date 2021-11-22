using System.Threading.Tasks;

namespace clone_aviasales.Data.Repository
{
    public interface ITicketsRepository
    {
        public Task<TicketsResponse> FetchTickets(TicketRequest request);
    }
}
