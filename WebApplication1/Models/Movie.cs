namespace WebApplication1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? Popularity { get; set; }
        public int? Budget { get; set; }
        public string Homepage { get; set; }
        public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
    }

}