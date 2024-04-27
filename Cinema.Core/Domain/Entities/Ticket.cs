namespace Cinema.Core.Domain.Entities
{
    public partial class Ticket : BaseEntity
    {
        public int SeatRow { get; set; }

        public int SeatNumber { get; set; }

        //public Guid UserId { get; set; }

        public Guid SessionId { get; set; }

        public Session Session { get; set; }
    }
}