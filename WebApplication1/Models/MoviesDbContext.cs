using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().ToTable("person")
                .Property(a => a.Id).HasColumnName("person_id");

            modelBuilder.Entity<Actor>()
                .Property(a => a.Name).HasColumnName("person_name");

            modelBuilder.Entity<Movie>().ToTable("movie")
                .Property(m => m.Id).HasColumnName("movie_id");

            modelBuilder.Entity<Movie>()
                .Property(m => m.Title).HasColumnName("title");

            modelBuilder.Entity<Movie>()
                .Property(m => m.Popularity).HasColumnName("popularity");

            modelBuilder.Entity<Movie>().Property(m => m.Budget).HasColumnName("budget");

            modelBuilder.Entity<Movie>().Property(m => m.Homepage).HasColumnName("homepage");

            modelBuilder.Entity<MovieCast>().ToTable("movie_cast")
                .HasKey(mc => new { mc.MovieId, mc.PersonId });

            modelBuilder.Entity<MovieCast>()
                .Property(mc => mc.MovieId).HasColumnName("movie_id");

            modelBuilder.Entity<MovieCast>()
                .Property(mc => mc.PersonId).HasColumnName("person_id");

            modelBuilder.Entity<MovieCast>()
                .Property(mc => mc.CharacterName).HasColumnName("character_name");

            modelBuilder.Entity<MovieCast>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCasts)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCast>()
                .HasOne(mc => mc.Actor)
                .WithMany(a => a.MovieCasts)
                .HasForeignKey(mc => mc.PersonId);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

    }
}