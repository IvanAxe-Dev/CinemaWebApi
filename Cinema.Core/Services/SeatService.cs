﻿using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class SeatService : Service<Seat>
    {
        public SeatService(IRepository<Seat> seatRepository) : base(seatRepository)
        {
        }
    }
}
