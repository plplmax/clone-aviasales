using clone_aviasales.Domain.Model;
using System;
using System.Linq;

namespace clone_aviasales.Domain.Interactors
{
    public class FilterTicketsInteractor
    {
        public TicketsResponse Execute(TicketsFilters filters, TicketsResponse response)
        {
            if (filters.Transfers != null && filters.Transfers.Any())
            {
                response.Data = response.Data.FindAll(ticket => filters.Transfers.Contains(ticket.Transfers));
            }
            if (filters.Airlines != null && filters.Airlines.Any())
            {
                response.Data = response.Data.FindAll(ticket => filters.Airlines.Contains(ticket.Airline));
            }
            if (filters.DurationInHours != default(short))
            {
                response.Data = response.Data.FindAll(ticket => filters.DurationInHours * 60 >= ticket.Duration);
            }
            return response;
        }
    }
}
