using Cinema.Core.Domain.Entities;

namespace Cinema.Core.Domain.RepositoryContracts
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task PostRange(List<Ticket> entity);
    }
}
