using Microsoft.EntityFrameworkCore;
using PersonRecommendationApp.Models.Entities;

namespace PersonRecommendationApp.Services;

public class DbPersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _db;

    public DbPersonRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<Recommendation> CreateRecommendationAsync(
        int personId, Recommendation recommendation)
    {
        var person = await ReadAsync(personId);
        if(person != null)
        {
            person.Recommendations.Add(recommendation);
            recommendation.Person = person;
            await _db.SaveChangesAsync();
        }
        return recommendation;
    }

    public async Task<ICollection<Person>> ReadAllAsync()
    {
        return await _db.People
            .Include(p => p.Recommendations)
            .ToListAsync();
    }

    public async Task<Person?> ReadAsync(int id)
    {
        return await _db.People
            .Include(p => p.Recommendations)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}