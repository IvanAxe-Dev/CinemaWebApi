using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class SeatService : Service<Seat>, ISeatService
    {
        private readonly IMapper _mapster;

        public SeatService(ISeatRepository repository, IMapper mapster) : base(repository)
        {
            _mapster = mapster;
        }
    }
}