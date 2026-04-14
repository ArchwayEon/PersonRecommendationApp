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
            recommendation.Id = 0;
            recommendation.Person = person;
            _db.Recommendations.Add(recommendation);
            await _db.SaveChangesAsync();
        }
        return recommendation;
    }

    public async Task DeleteRecommendationAsync(int recommendationId)
    {
        Recommendation? recToDelete = 
            await _db.Recommendations.FirstOrDefaultAsync(
                r => r.Id == recommendationId);
        if(recToDelete != null)
        {
            _db.Recommendations.Remove(recToDelete);
            await _db.SaveChangesAsync();
        }
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

    public async Task UpdateRecommendationAsync(Recommendation recommendation)
    {
        Recommendation? recToUpdate = 
            await _db.Recommendations.FirstOrDefaultAsync(
                r => r.Id == recommendation.Id);
        if(recToUpdate != null)
        {
            recToUpdate.Rating = recommendation.Rating;
            recToUpdate.Narrative = recommendation.Narrative;
            await _db.SaveChangesAsync();
        }
    }
}