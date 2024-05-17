namespace Cinema.Core.DTO
{
    public record GetMoviesQuery(string? SearchTerm, string? Categories, DateTime? DateStartInterval, DateTime? DateEndInterval, DateTime? TimeStartInterval, DateTime? TimeEndInterval);
}
