namespace WebApplication1.Controllers
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieCount { get; set; }
        public string MostPopularMovie { get; set; }
        public List<string> PopularMovieCharacters { get; set; } = new List<string>();
    }
}