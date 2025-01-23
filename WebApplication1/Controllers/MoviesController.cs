using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesDbContext _context;

        public MoviesController(MoviesDbContext context)
        {
            _context = context;
            Console.WriteLine($"Using database at: {_context.Database.GetConnectionString()}");
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            try
            {
                var totalActors = await _context.MovieCasts
                    .AsNoTracking()
                    .Select(mc => mc.PersonId)
                    .Distinct()
                    .CountAsync();
                
                var movieCasts = await _context.MovieCasts
                    .AsNoTracking()
                    .Include(mc => mc.Movie)
                    .Include(mc => mc.Actor)
                    .ToListAsync();
                
                var groupedActors = movieCasts
                    .GroupBy(mc => mc.PersonId)
                    .OrderBy(g => g.FirstOrDefault()?.Actor?.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(g => new ActorDetailsViewModel
                    {
                        Id = g.Key,
                        Name = g.FirstOrDefault()?.Actor?.Name,
                        MovieCount = g.Count(),
                        MostPopularMovie = g.OrderByDescending(mc => mc.Movie.Popularity)
                            .Select(mc => mc.Movie.Title)
                            .FirstOrDefault(),
                        PopularMovieCharacters = g
                            .OrderByDescending(mc => mc.Movie.Popularity)
                            .Select(mc => mc.CharacterName)
                            .Take(3)
                            .ToList()
                    })
                    .ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(totalActors / (double)pageSize);

                return View(groupedActors);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching actors: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        public async Task<IActionResult> ActorMovies(int id, int page = 1, int pageSize = 20, int returnPage = 1)
        {
            try
            {
                var actor = await _context.Actors
                    .AsNoTracking()
                    .Include(a => a.MovieCasts)
                    .ThenInclude(mc => mc.Movie)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (actor == null)
                {
                    return NotFound();
                }

                var totalMovies = actor.MovieCasts.Count;
                
                var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);
                if (page > totalPages && totalPages > 0)
                {
                    page = totalPages;
                }

                var movies = actor.MovieCasts
                    .OrderBy(mc => mc.Movie.Title)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(mc => new MovieViewModel
                    {
                        Id = mc.Movie.Id,
                        Title = mc.Movie.Title,
                        Budget = mc.Movie.Budget ?? 0,
                        Popularity = mc.Movie.Popularity ?? 0,
                        Homepage = mc.Movie.Homepage
                    })
                    .ToList();

                ViewBag.ActorName = actor.Name;
                ViewBag.ActorId = actor.Id;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.ReturnPage = returnPage;

                return View(movies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movies for actor {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet]
        public IActionResult AddMovie(int actorId)
        {
            ViewBag.ActorId = actorId;
            ViewBag.Movies = _context.Movies
                .AsNoTracking()
                .Select(m => new { m.Id, m.Title })
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(int actorId, int movieId, string roleName)
        {
            try
            {
                var movieCast = new MovieCast
                {
                    PersonId = actorId,
                    MovieId = movieId,
                    CharacterName = roleName
                };

                _context.MovieCasts.Add(movieCast);
                await _context.SaveChangesAsync();

                return RedirectToAction("ActorMovies", new { id = actorId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Budget { get; set; }
        public double Popularity { get; set; }
        public string Homepage { get; set; }
    }

    public class ActorDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieCount { get; set; }
        public string MostPopularMovie { get; set; }
        public List<string> PopularMovieCharacters { get; set; } = new List<string>();
    }
}
