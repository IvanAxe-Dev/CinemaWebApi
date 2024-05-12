using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        private readonly IMapper _mapster;

        public TicketService(ITicketRepository repository, IMapper mapster) : base(repository)
        {
            _mapster = mapster;
        }
        //datetime now on post for bookedat property
    }
}