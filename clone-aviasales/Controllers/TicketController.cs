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

        public TicketController(FetchTicketsInteractor fetchTickets)
        {
            this.fetchTickets = fetchTickets;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketRequest request)
        {
            TicketsResponse response = await fetchTickets.Execute(request);
            return Json(response);
        }
    }
}
