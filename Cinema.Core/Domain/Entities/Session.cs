namespace Cinema.Core.Domain.Entities
{
    //one session is <movie> at <time> on <weekday, date>
    public partial class Session : BaseEntity
    {
        public DateOnly? Date { get; set; }

        public TimeOnly? StartTime { get; set; }

        public decimal? Price { get; set; }

    }
}