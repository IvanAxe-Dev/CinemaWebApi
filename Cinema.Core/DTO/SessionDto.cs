namespace Cinema.Core.DTO;

public class SessionDto
{
    public string Date { get; set; }
    
    public string StartTime { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Guid CinemaHallId { get; set; }
}