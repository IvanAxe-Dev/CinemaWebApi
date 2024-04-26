using Cinema.Infrastructure.DatabaseContext;

namespace Cinema.Core.DTO;

public class MovieDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Image { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string Director { get; set; }
    
    public TimeOnly Duration { get; set; }
    
    public string TrailerUrl { get; set; }
    
    public string Actors { get; set; }
    
    public double Rating { get; set; }
    
    public List<Session> Sessions { get; set; }
}