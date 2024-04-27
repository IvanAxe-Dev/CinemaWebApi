using Cinema.Core.Enums;

namespace Cinema.Core.Domain.Entities
{
    public partial class CinemaHall : BaseEntity
    {
        public Graphics? Graphics { get; set; }

        public Privilege? Privilege { get; set; }

        public byte[][] Seats { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
