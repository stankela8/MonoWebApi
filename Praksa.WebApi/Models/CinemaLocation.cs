using System;

namespace Praksa.WebApi.Models
{
    public class CinemaLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Hall> Halls { get; set; } = new List<Hall>();
    }
}
