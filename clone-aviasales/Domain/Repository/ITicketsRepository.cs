using clone_aviasales.Domain.Model;
using System.Threading.Tasks;

namespace clone_aviasales.Domain.Repository
{
    public interface ITicketsRepository
    {
        public Task<TicketsResponse> FetchTickets(TicketRequest request);
    }
}
