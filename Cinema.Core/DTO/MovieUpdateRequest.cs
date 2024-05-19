using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.DTO
{
    public class MovieUpdateRequest
    {
        public string? Title { get; set; }

        public DateTime? RentalStartDate { get; set; }

        public DateTime? RentalEndDate { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Director { get; set; }

        public string Duration { get; set; }

        public int? AgeRestriction { get; set; }

        public string? TrailerUrl { get; set; }

        public string? Actors { get; set; }

       
    }
}
