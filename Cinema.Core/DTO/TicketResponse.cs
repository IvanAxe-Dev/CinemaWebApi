namespace Cinema.Core.DTO;

public class TicketResponse
{
    public Guid Id { get; set; }
    public Guid SeatId { get; set; }
    public Guid SessionId { get; set; }
}