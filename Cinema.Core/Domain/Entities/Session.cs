namespace Cinema.Core.Domain.Entities
{
    //one session is <movie> at <time> on <weekday, date>
    public partial class Session : BaseEntity
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public Movie Movie { get; set; }
        public Guid CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }
    }
}