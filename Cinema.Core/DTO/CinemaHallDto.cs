using Cinema.Core.Enums;

namespace Cinema.Core.DTO;

public class CinemaHallDto
{
    public Graphics? Graphics { get; set; }
    public Privilege? Privilege { get; set; }
    public int? SeatsCount { get; set; }
    public ICollection<Guid> SeatsId { get; set; } = new List<Guid>();
    public ICollection<Guid> SessionsId { get; set; } = new List<Guid>();
}