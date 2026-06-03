namespace Praksa.WebApi.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int CinemaLocationId { get; set; }
        public CinemaLocation CinemaLocation { get; set; }

        public List<Projection> Projections { get; set; } = new List<Projection>();
    }
}
