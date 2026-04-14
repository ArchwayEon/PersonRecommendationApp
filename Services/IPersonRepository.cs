using Microsoft.AspNetCore.Routing.Constraints;
using PersonRecommendationApp.Models.Entities;

namespace PersonRecommendationApp.Services;

public interface IPersonRepository
{
    Task<ICollection<Person>> ReadAllAsync();
    Task<Person?> ReadAsync(int id);
    Task<Recommendation> CreateRecommendationAsync(
        int personId, Recommendation recommendation);
    Task UpdateRecommendationAsync(Recommendation recommendation);
    Task DeleteRecommendationAsync(int recommendationId);
}
