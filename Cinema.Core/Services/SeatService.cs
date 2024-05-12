using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class SeatService : Service<Seat>, ISeatService
    {
        private readonly IMapper _mapster;
        private readonly ISeatRepository _seatRepository;
        private readonly ISessionService _sessionService;

        public SeatService(ISeatRepository repository, IMapper mapster, ISeatRepository seatRepository, ISessionService sessionService) : base(repository)
        {
            _mapster = mapster;
            _seatRepository = seatRepository;
            _sessionService = sessionService;
        }

        public async Task<List<SeatResponse>> CreateSeatsForSession(Guid sessionId)
        {
            var session = await _sessionService.FindByIdAsync(sessionId);

            List<Seat> seats = new List<Seat>();    

            for (int row = 1; row <= session.CinemaHall.RowsCount; row++)
            {
                for (int number = 1; number <= session.CinemaHall.NumbersCount; number++)
                {
                    seats.Add(new Seat { Row = row, Number = number, SessionId = session.Id });
                }
            }

            await _seatRepository.PostRange(seats);
            await _seatRepository.SaveChangesAsync();

            return _mapster.Map<List<SeatResponse>>(seats); ;
        }

    }
}