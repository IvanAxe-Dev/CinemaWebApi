namespace Cinema.Core.DTO;

public class SeatDto
{
    public int Row { get; set; }
    public int Number { get; set; }
    public bool IsBooked { get; set; }
    public Guid SessionId { get; set; }
}