using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }

        protected override IQueryable<Session> PrepareDbSet()
        {
            return base.PrepareDbSet()
                .Include(x => x.Movie)
                .Include(x => x.CinemaHall)
                .Include(x => x.Seats);
        }
    }
}
