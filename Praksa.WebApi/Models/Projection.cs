namespace Praksa.WebApi.Models
{
    public class Projection
    {
        public int Id { get; set; }
        public DateTime ShowTime { get; set; }
        public double TicketPrice { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
