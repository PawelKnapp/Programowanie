namespace WebApplication1.Models
{
    public class MovieCast
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int PersonId { get; set; }
        public Actor Actor { get; set; }

        public string CharacterName { get; set; }
    }

}