using Microsoft.EntityFrameworkCore;
using PersonRecommendationApp.Models.Entities;

namespace PersonRecommendationApp.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> People => Set<Person>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
}
