namespace Cinema.Core.Domain.Entities;

public partial class Session : BaseEntity
{

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Price { get; set; }

    public int? CinemaHall { get; set; }

    public string? Graphics { get; set; }

    public int? Seats { get; set; }

    public Guid? MovieId { get; set; }

    public string? Privilege { get; set; }

    public virtual Movie? Movie { get; set; }
}
