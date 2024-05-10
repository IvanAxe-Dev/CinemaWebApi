namespace Cinema.Core.DTO;

public class TicketDto
{
    public Guid SeatId { get; set; }
    public Guid SessionId { get; set; }
    public Guid ApplicationUserId { get; set; }
}