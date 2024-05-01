using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }

        protected override IQueryable<Ticket> PrepareDbSet()
        {
            return base.PrepareDbSet()
                .Include(x => x.Seat)
                .Include(x => x.Session);
        }
    }
}
