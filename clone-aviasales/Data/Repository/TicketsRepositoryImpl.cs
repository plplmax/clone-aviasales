using clone_aviasales.Data.Source;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace clone_aviasales.Data.Repository
{
    class TicketsRepositoryImpl : ITicketsRepository
    {
        private readonly TicketsCloudDataSource cloudDataSource;

        public TicketsRepositoryImpl(TicketsCloudDataSource cloudDataSource)
        {
            this.cloudDataSource = cloudDataSource;
        }

        public async Task<TicketsResponse> FetchTickets(TicketRequest request)
        {
            try
            {
                string result = await cloudDataSource.FetchTickets(request);
                return JsonSerializer.Deserialize<TicketsResponse>(result);
            }
            catch (Exception e)
            {
                return new TicketsResponse() { Success = false, Error = e.Message };
            }
        }
    }
}
