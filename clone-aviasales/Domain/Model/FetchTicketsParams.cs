namespace clone_aviasales.Domain.Model
{
    public class FetchTicketsParams
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureAt { get; set; }
        public string ReturnAt { get; set; }
        public string Currency { get; set; }
    }
}
