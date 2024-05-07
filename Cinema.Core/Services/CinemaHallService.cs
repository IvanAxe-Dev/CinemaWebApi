using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class CinemaHallService : Service<CinemaHall>, ICinemaHallService
    {
        private readonly IMapper _mapster;
        private readonly ISeatService _seatService;

        public CinemaHallService(ICinemaHallRepository repository, IMapper mapster, ISeatService seatService) : base(repository)
        {
            _mapster = mapster;
            _seatService = seatService;
        }
    }
}