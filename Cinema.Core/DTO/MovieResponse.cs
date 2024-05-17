namespace Cinema.Core.DTO;

public class MovieResponse
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }

    public DateTime? RentalStartDate { get; set; }

    public DateTime? RentalEndDate { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Director { get; set; }

    public string Duration { get; set; }

    public int? AgeRestriction { get; set; }

    public string? TrailerUrl { get; set; }

    public string? Actors { get; set; }

    public List<int>? Ratings { get; set; }

    public int? UserRating { get; set; }

    public List<SessionResponse> Sessions { get; set; } = new List<SessionResponse>();

    public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
}