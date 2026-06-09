namespace Praksa.WebApi.Models
{
    public class FootballClubUpsertRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundedYear { get; set; }
    }
}