using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class AppDbContext: DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source = {DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>().HasData(
            new ContactEntity()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Kowal",
                BirthDate = new DateOnly(2000, 10, 10),
                Email = "adam@op.pl",
                PhoneNumber = "123456789",
                Created = DateTime.Now
            },
            new ContactEntity()
            {
                Id = 2,
                FirstName = "Ewa",
                LastName = "Kowal",
                BirthDate = new DateOnly(2000, 10, 10),
                Email = "ewa@op.pl",
                PhoneNumber = "123456789",
                Created = DateTime.Now
            }
            );
    }
}