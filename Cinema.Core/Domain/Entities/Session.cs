namespace Cinema.Core.Domain.Entities
{
    //one session is <movie> at <time> on <weekday, date>
    public partial class Session : BaseEntity
    {
        public DateTime Date { get; set; }
        
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
        
        public int AvailableTickets { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }
    }
}