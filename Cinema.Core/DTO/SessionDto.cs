namespace Cinema.Core.DTO;

public class SessionDto
{
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public decimal Price { get; set; }
    
    public int AvailableSeats { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Guid CinemaHallId { get; set; }
}