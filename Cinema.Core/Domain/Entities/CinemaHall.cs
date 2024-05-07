using Cinema.Core.Enums;
using Newtonsoft.Json;

namespace Cinema.Core.Domain.Entities
{
    public partial class CinemaHall : BaseEntity
    {
        public Graphics? Graphics { get; set; }
        public Privilege? Privilege { get; set; }
        public int? RowsCount { get; set; }
        public int? NumbersCount { get; set; }

        [JsonIgnore]
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        [JsonIgnore]
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
