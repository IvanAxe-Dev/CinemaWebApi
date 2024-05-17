namespace Cinema.Core.DTO;

public class TicketResponse
{
    public int Row { get; set; }
    public int Number { get; set; }
    public bool IsBooked { get; set; }
    public DateTime BookedAt { get; set; }
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
}