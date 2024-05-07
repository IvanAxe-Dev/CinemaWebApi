using Cinema.Core.Domain.Entities;

namespace Cinema.Core.DTO;

public class MovieDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Image { get; set; }
    
    public string ReleaseDate { get; set; }
    
    public string Director { get; set; }
    
    public string Duration { get; set; }
    
    public string TrailerUrl { get; set; }
    
    public string Actors { get; set; }
    
    public List<int> Ratings { get; set; }
    
    public List<Session> Sessions { get; set; }
}