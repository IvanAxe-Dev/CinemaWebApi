using Cinema.Core.Domain.Entities;

namespace Cinema.Core.DTO;

public class SessionResponse
{
    public Guid Id { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
    public Guid MovieId { get; set; }
    public Guid CinemaHallId { get; set; }
}