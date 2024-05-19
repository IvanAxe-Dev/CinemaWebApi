namespace Cinema.Core.Domain.Entities
{
    public partial class Ticket : BaseEntity
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; } = false;
        public DateTime BookedAt { get; set; }
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
        public Guid? ApplicationUserId { get; set; }

        
    }
}