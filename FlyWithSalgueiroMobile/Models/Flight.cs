namespace FlyWithSalgueiroMobile.Models
{
    public class Flight
    {
        public int Id { get; set; }

        public string? FlightNumber { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public TimeSpan FlightDuration { get; set; }

        public City? Origin { get; set; }

        public City? Destination { get; set; }

        public string? OriginAirport { get; set; }

        public string? DestinationAirport { get; set; }

        public int AvailableSeatsNumber { get; set; }
    }
}
