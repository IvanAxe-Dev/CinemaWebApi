using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Enums;
using Newtonsoft.Json;

namespace Cinema.Core.DTO
{
    public class CinemaHallResponse
    {
        public Guid Id { get; set; }
        public Graphics? Graphics { get; set; }
        public Privilege? Privilege { get; set; }
        public int? RowsCount { get; set; }
        public int? NumbersCount { get; set; }
        public ICollection<SessionResponse> Sessions { get; set; } = new List<SessionResponse>();
    }
}
