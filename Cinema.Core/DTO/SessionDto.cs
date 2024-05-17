namespace Cinema.Core.DTO;

public class SessionDto
{
    public DateTime Date { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Guid CinemaHallId { get; set; }
}