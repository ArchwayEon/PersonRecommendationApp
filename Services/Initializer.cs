using PersonRecommendationApp.Models.Entities;

namespace PersonRecommendationApp.Services;
public class Initializer
{
    private readonly ApplicationDbContext _db;

    public Initializer(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task SeedDatabaseAsync()
    {
        _db.Database.EnsureCreated();

        // If there are any students then assume the database is already
        // seeded.
        if (_db.People.Any()) return;

        var people = new List<Person>
        {
           new() { FirstName = "James", LastName = "Smith", DateOfBirth = new DateTime(2000, 1, 1) },
           new() { FirstName = "Maria", LastName = "Garcia", DateOfBirth = new DateTime(2001, 2, 1) },
           new() { FirstName = "Chen", LastName = "Li", DateOfBirth = new DateTime(2002, 3, 1) },
           new() { FirstName = "Aban", LastName = "Hakim", DateOfBirth = new DateTime(2003, 4, 1) }
        };

        await _db.People.AddRangeAsync(people);
        await _db.SaveChangesAsync();
    }
}
