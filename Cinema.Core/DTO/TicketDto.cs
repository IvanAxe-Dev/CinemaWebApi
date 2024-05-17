namespace Cinema.Core.DTO;

public class TicketDto
{
    public int Row { get; set; }
    public int Number { get; set; }
    public Guid SessionId { get; set; }
    public Guid ApplicationUserId { get; set; }
}