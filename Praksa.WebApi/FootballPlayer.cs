namespace Praksa.WebApi
{
    public class FootballPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }
        public int JerseyNumber { get; set; }
        public string Position { get; set; }
        public double MarketValue { get; set; }
    }
}