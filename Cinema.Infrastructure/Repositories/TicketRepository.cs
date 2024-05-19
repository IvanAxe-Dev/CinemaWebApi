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
                .Include(x => x.Session)
                .Include(x => x.Session.CinemaHall);
        }
        public async Task PostRange(List<Ticket> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbSet.AddRangeAsync(entity);
        }
    }
}
