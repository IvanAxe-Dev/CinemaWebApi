using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain.Entities
{
    public class Seat : BaseEntity
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; }
        public Guid CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } 
    }
}
