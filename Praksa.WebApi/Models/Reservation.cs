namespace Praksa.WebApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TicketCount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectionId { get; set; }
        public Projection Projection { get; set; }
    }
}
