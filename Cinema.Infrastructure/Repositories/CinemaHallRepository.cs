﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class CinemaHallRepository : Repository<CinemaHall>, ICinemaHallRepository
    {
        public CinemaHallRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }

        protected override IQueryable<CinemaHall> PrepareDbSet()
        {
            return base.PrepareDbSet()
                .Include(x => x.Seats)
                .Include(x=>x.Sessions);
        }
    }
}
