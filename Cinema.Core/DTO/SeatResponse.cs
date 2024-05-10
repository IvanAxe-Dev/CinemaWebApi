using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;

namespace Cinema.Core.DTO
{
    public class SeatResponse : BaseEntity
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; }
    }
}
