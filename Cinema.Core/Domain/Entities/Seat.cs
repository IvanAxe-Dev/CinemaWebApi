using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cinema.Core.Domain.Entities
{
    public class Seat : BaseEntity
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; } = false;

        public Guid SessionId { get; set; }
        public Session Session { get; set; }
    }
}
