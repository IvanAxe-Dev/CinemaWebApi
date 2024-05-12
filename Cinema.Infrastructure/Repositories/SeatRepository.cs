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
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }

        protected override IQueryable<Seat> PrepareDbSet()
        {
            return base.PrepareDbSet()
                .Include(x => x.Session);
        }
    }
}
