using Cinema.Core.Domain.Entities;

namespace Cinema.Core.DTO;

public class MovieCategoryResponse
{
    public Guid Id { get; set; }

    public CategoryResponse Category { get; set; }

    public MovieResponse Movie { get; set; }
}