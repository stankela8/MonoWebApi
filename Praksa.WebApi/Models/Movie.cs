namespace Praksa.WebApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }

        public List<Projection> Projections { get; set; } = new List<Projection>();
    }
}
