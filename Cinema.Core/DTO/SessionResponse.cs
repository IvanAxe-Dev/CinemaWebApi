using Cinema.Core.Domain.Entities;

namespace Cinema.Core.DTO;

public class SessionResponse
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
    public Guid MovieId { get; set; }
    public Guid CinemaHallId { get; set; }
    public List<TicketResponse> Tickets { get; set; } = new List<TicketResponse>();

}