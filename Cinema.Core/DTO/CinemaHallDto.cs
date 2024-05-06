using Cinema.Core.Enums;

namespace Cinema.Core.DTO;

public class CinemaHallDto
{
    public Guid Id { get; set; }
    public Graphics? Graphics { get; set; }
    public Privilege? Privilege { get; set; }
    public int? SeatsCount { get; set; }
}