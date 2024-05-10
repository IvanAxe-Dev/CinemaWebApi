using Cinema.Core.Enums;

namespace Cinema.Core.DTO;

public class CinemaHallDto
{
    public Graphics? Graphics { get; set; }
    public Privilege? Privilege { get; set; }
    public int? RowsCount { get; set; }
    public int? NumbersCount { get; set; }
}