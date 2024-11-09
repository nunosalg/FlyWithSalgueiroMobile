namespace FlyWithSalgueiroMobile.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        public string? TicketBuyer { get; set; }

        public string? FlightNumber { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public string? Origin { get; set; }

        public string? Destination { get; set; }

        public string? OriginAirport { get; set; }

        public string? DestinationAirport { get; set; }

        public string? Seat { get; set; }

        public string? PassengerName { get; set; }

        public string? PassengerId { get; set; }

        public decimal Price { get; set; }

        public string? Status { get; set; }
    }
}
