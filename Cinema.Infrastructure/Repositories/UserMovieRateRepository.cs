using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class UserMovieRateRepository : Repository<UserMovieRate>, IUserMovieRateRepository
    {
        public UserMovieRateRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }

        protected override IQueryable<UserMovieRate> PrepareDbSet()
        {
            return base.PrepareDbSet()
                .Include(x => x.Movie);
        }
    }
}
