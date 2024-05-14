using Cinema.Core.Domain.Entities;

namespace Cinema.Core.Domain.RepositoryContracts
{
    public interface ISeatRepository : IRepository<Seat>
    {
        Task PostRange(List<Seat> entity);
    }
}
