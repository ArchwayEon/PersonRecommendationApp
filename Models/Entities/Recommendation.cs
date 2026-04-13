using System.ComponentModel.DataAnnotations;

namespace PersonRecommendationApp.Models.Entities;

public enum Rating
{
    Poor, Mediocre, Fair, Good, Excellent
}

public class Recommendation
{
    public int Id { get; set; }
    public Rating Rating { get; set; }
    [StringLength(512)]
    public string Narrative { get; set; } = String.Empty;
    public int PersonId { get; set; }
    public Person? Person { get; set; }
    
}

