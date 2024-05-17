using System.ComponentModel;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        private readonly IMapper _mapster;
        private readonly ISessionService _sessionService;
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository repository, IMapper mapster, ISessionService sessionService, ITicketRepository ticketRepository) : base(repository)
        {
            _mapster = mapster;
            _sessionService = sessionService;
            _ticketRepository = ticketRepository;
        }
        public async Task<List<TicketResponse>> CreateTicketsForSession(Guid sessionId)
        {
            var session = await _sessionService.FindByIdAsync(sessionId);

            List<Ticket> tickets = new List<Ticket>();

            for (int row = 1; row <= session.CinemaHall.RowsCount; row++)
            {
                for (int number = 1; number <= session.CinemaHall.NumbersCount; number++)
                {
                    tickets.Add(new Ticket() { Row = row, Number = number, SessionId = session.Id });
                }
            }

            await _ticketRepository.PostRange(tickets);
            await _ticketRepository.SaveChangesAsync();

            return _mapster.Map<List<TicketResponse>>(tickets);
        }
    }
}