using clone_aviasales.Domain.Interactors;
using clone_aviasales.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clone_aviasales.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly FetchTicketsInteractor fetchTickets;
        private readonly FilterTicketsInteractor filterTickets;

        public TicketController(FetchTicketsInteractor fetchTickets, FilterTicketsInteractor filterTickets)
        {
            this.fetchTickets = fetchTickets;
            this.filterTickets = filterTickets;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketRequest request)
        {
            TicketsResponse response = await fetchTickets.Execute(request);
            if (request.Filters == null) return Json(response);
            TicketsResponse filterResponse = filterTickets.Execute(request.Filters, response);
            return Json(filterResponse);
        }
    }
}
