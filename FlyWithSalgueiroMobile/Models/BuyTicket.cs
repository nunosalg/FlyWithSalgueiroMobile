namespace FlyWithSalgueiroMobile.Models
{
    public class BuyTicket
    {
        public int FlightId { get; set; }

        public string? Seat { get; set; }

        public string? PassengerName { get; set; }

        public string? PassengerId { get; set; }

        public DateTime PassengerBirthDate { get; set; }

        public decimal Price { get; set; }
    }
}
