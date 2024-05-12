using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;

namespace Cinema.Core.ServiceContracts
{
    public interface ISeatService : IService<Seat>
    {
        Task<List<SeatResponse>> CreateSeatsForSession(Guid sessionId);
    }
}
