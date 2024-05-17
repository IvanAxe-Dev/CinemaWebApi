using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;

namespace Cinema.Core.ServiceContracts
{
    public interface ITicketService : IService<Ticket>
    {
        Task<List<TicketResponse>> CreateTicketsForSession(Guid sessionId);
    }
}