namespace Cinema.Core.Domain.Entities
{
    public partial class Ticket : BaseEntity
    {
        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
    }
}