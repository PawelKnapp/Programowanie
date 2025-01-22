using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
    }
}