using Cinema.Core.Enums;

namespace Cinema.Core.Domain.Entities
{
    public partial class CinemaHall : BaseEntity
    {
        public Graphics? Graphics { get; set; }
        public Privilege? Privilege { get; set; }
        public int? SeatsCount { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
